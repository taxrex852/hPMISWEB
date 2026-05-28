# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**hPMISWEB** 是一個鋼鐵廠生產管理資訊系統（Production Management Information System），部署於 IIS，監控熱軋、高爐、焦化等製程的即時生產數據。

- **主專案**：VB.NET + ASP.NET WebForms，目標框架 .NET 4.0
- **Task 子系統**：C# + ASP.NET WebForms，目標框架 .NET 4.5，位於 `Task/` 目錄
- **資料庫**：SQL Server，本機 HPMIS、HBMPMIS 兩個 DB，加上遠端 Y6P2_Materials

## Build & Run

```powershell
# 在 Visual Studio 中開啟方案
# c:\inetpub\wwwroot\hPMISWEB.sln

# IIS 部署（開發環境）
# 主應用：http://localhost/hPMISWEB（入口：home.aspx）
# Task 子系統：http://localhost/hPMISWEB/Task/（入口：login.aspx）
```

MSBuild（命令列建置）：

```powershell
msbuild hPMISWEB.vbproj /p:Configuration=Debug
msbuild Task\WebApplication21.csproj /p:Configuration=Debug
```

沒有自動化測試套件。

## Architecture

### 核心模組（`*.vb` 共用模組）

| 檔案 | 用途 |
|------|------|
| [modSQL.vb](modSQL.vb) | 資料存取層：`execQuery`、`execSQL`、`getData`、`execReader` |
| [modGeneral.vb](modGeneral.vb) | 通用工具：MD5、連線管理、訊息函式 |
| [modDLDeclare.vb](modDLDeclare.vb) | 資料庫欄位 Enum 定義（~2400 行），涵蓋所有資料表欄位索引 |
| [modPageDeclare.vb](modPageDeclare.vb) | 系統常數：加密金鑰、Timeout 設定 |
| [Global.asax.vb](Global.asax.vb) | 應用程式啟動，初始化 PMIS 與 HBMPMIS 連線字串 |

### 頁面編號系統

頁面以數字 ID 命名，對應不同製程子系統：

| 範圍 | 子系統 |
|------|--------|
| 30xx | HSM 熱軋即時監控 |
| 31xx | HSM 生產追蹤 |
| 32xx | HSM 附加指標 |
| 34xx | 鋼鐵設施製程（高爐、焦化等） |
| 35xx | HSM 二次製程 |
| 36xx | 其他監控 |
| 37xx | 新開發功能 |

### 資料存取模式

所有 DB 操作透過 `modSQL.vb` 的共用函式，直接使用 `SqlClient`（無 ORM）。欄位以 `modDLDeclare.vb` 中的 Enum 索引取值，例如：

```vb
Dim dt As DataTable = execQuery("SELECT ... FROM AP10", cnPMIS)
Dim val = dt.Rows(0)(AP10.col_name)  ' 使用 Enum 取欄位
```

連線物件 `cnPMIS`、`cnHBMPMIS` 在 `Global.asax.vb` 中初始化為全域變數。

### Task 子系統

獨立的 C# 專案（`Task/WebApplication21.csproj`），功能包含工單管理、議題追蹤、Crystal Reports 報表。與主專案共用 IIS 虛擬目錄但獨立建置。

### 共用 UI 元件

- [include/header.ascx](include/header.ascx)：所有頁面共用的頁首控制項
- AJAX UpdatePanel 用於局部刷新
- ECharts（JS）用於趨勢圖表
- TeeChart（v4.1）用於特殊圖表

## Key Dependencies

- **Crystal Reports** v13.0.4000.0（報表，Task 子系統）
- **iTextSharp** v5.5.13.3（PDF 輸出）
- **AjaxControlToolkit**（AJAX 控制項）
- **TeeChart** v4.1（圖表）
- **BouncyCastle.Crypto** v1.8.9（加解密）

DLL 皆置於 `bin/` 目錄，組件繫結設定在 `Web.config` 的 `<assemblyBinding>`。

## Database Connection Strings

定義於 `Web.config`（`<connectionStrings>`）：

- `PMISConnectionString`：`127.0.0.1`，DB `HPMIS`
- `HBMPMISConnectionString`：`127.0.0.1`，DB `HBMPMIS`
- `Y6P2_MaterialsConnectionString`：`10.108.20.4`，DB `Y6P2_Materials`

## Code Conventions

- 變數名稱、註解多為繁體中文
- 頁面 code-behind 皆以 `.aspx.vb`（主專案）或 `.aspx.cs`（Task）命名
- Designer 自動產生檔：`.aspx.designer.vb`，不需手動編輯
- 所有頁面皆繼承 `System.Web.UI.Page`，不使用 Master Pages
