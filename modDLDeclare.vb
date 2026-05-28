Module modDLDeclare
    Public Const cntHeader = 4 'Header的欄位個數
    '格式 Header + 欄位數 + rowguid
    Public Const cntAP10 = cntHeader + 27 + 1
    Public Const cntAP11 = cntHeader + 74 + 1
    Public Const cntBP10 = cntHeader + 31 + 1
    Public Const cntBP11 = cntHeader + 107 + 1
    Public Const cntBP12 = cntHeader + 22 + 1
    Public Const cntBP13 = cntHeader + 28 + 1
    Public Const cntCP10 = cntHeader + 10 + 1
    Public Const cntCP11 = cntHeader + 43 + 1
    Public Const cntCP12 = cntHeader + 58 + 1
    Public Const cntCP13 = cntHeader + 51 + 1
    Public Const cntCP14_master = cntHeader + 3 + 1
    Public Const cntCP14_detail = cntHeader + 16 + 1
    Public Const cntCP15_master = cntHeader + 3 + 1
    Public Const cntCP15_detail = cntHeader + 6 + 1
    Public Const cntEP10 = cntHeader + 79 + 1
    Public Const cntPP10 = cntHeader + 5 + 1
    Public Const cntQP11 = cntHeader + 10 + 1
    Public Const cntQP12 = cntHeader + 15 + 1
    Public Const cntRP10 = cntHeader + 7 + 1
    Public Const cntRP12 = cntHeader + 13 + 1
    Public Const cntSP10 = cntHeader + 18 + 1
    Public Const cntSP11 = cntHeader + 141 + 1
    Public Const cntSP12 = cntHeader + 31 + 1
    Public Const cntWK1p = cntHeader + 53 + 1


    Public Const cntBP20_day = 17 + 1
    Public Const cntCP20_day = 15 + 1
    Public Const cntFP20_day = 17 + 1
    Public Const cntOP20_day = 18 + 1
    Public Const cntPP20_day = 4 + 1
    Public Const cntPP21_day = 14 + 1
    Public Const cntPP22_day = 9 + 1
    'Public Const cntQP20_day = 9
    Public Const cntRP20_day = 4 + 1
    Public Const cntSP20_day = 10 + 1
    Public Const cntSP75_day = 10 + 1
    Public Const cntUP20_day = 73 + 1

    'Public Const cntBP20_util = 17
    'Public Const cntCP20_util = 15
    'Public Const cntFP20_util = 17
    'Public Const cntOP20_util = 18
    'Public Const cntPP20_util = 4
    'Public Const cntPP21_util = 15
    'Public Const cntPP22_util = 9
    'Public Const cntRP20_util = 4
    'Public Const cntSP20_util = 10
    'Public Const cntSP75_util = 10
    'Public Const cntUP20_util = 72

    Public Const cntMS01 = cntHeader + 14 + 1
    Public Const cntMS03 = cntHeader + 20 + 1
    Public Const cntMS04 = cntHeader + 22 + 1
    Public Const cntMS05 = cntHeader + 34 + 1
    Public Const cntMS06 = cntHeader + 10 + 1
    Public Const cntMS08 = cntHeader + 26 + 1
    Public Const cntMS09 = cntHeader + 183 + 1
    Public Const cntMS10 = cntHeader + 40 + 1
    Public Const cntMS11 = cntHeader + 70 + 1
    Public Const cntMS12 = cntHeader + 15 + 1
    Public Const cntMS13 = cntHeader + 21 + 1
    Public Const cntMS14 = cntHeader + 5 + 1
    Public Const cntMS15 = cntHeader + 4 + 1
    Public Const cntPP31 = cntHeader + 9 + 1
    Public Const cntPP32 = cntHeader + 6 + 1
    Public Const cntPP33 = cntHeader + 14 + 1
    Public Const cntPP99 = cntHeader + 3 + 1
    Public Const cntSchedule = 6 + 1
    Public Const cntEaf_data = 19 + 1
    Public Const cntSld_data = 26 + 1
    Public Const cntSP74 = cntHeader + 21 + 1
    Public Const cntTQ75 = cntHeader + 17 + 1
    Public Const cntbf_limit = 33 + 1
    Public Const cntsp_limit = 4 + 1
    Public Const cntcok_limit = 1 + 1

    Public Const cntPc_Conn_Sts = 3 + 1

    Public Enum idxPc_Conn_Sts
        name = 0
        status = 1
        updatetime = 2
    End Enum

    'Index of AP10 Structure 
    Public Enum idxMsgAP10
        process_date = 1
        tpc_pw = cntHeader + 0
        dsc_pw = cntHeader + 1
        total_pw = cntHeader + 2
        avg_pw = cntHeader + 3
        peak_sale = cntHeader + 4
        offpeak_sale = cntHeader + 5
        tg1_rated = cntHeader + 6
        tg1_peak = cntHeader + 7
        tg1_avg = cntHeader + 8
        tg2_rated = cntHeader + 9
        tg2_peak = cntHeader + 10
        tg2_avg = cntHeader + 11
        trt1_rated = cntHeader + 12
        trt1_peak = cntHeader + 13
        trt1_avg = cntHeader + 14
        max_req = cntHeader + 15
        max_req_time = cntHeader + 16
        peak_max = cntHeader + 17
        peak_max_time = cntHeader + 18
        max_use = cntHeader + 19
        max_use_time = cntHeader + 20
        ph_load = cntHeader + 21
        wh_load = cntHeader + 22
        self_load = cntHeader + 23
        self_rate = cntHeader + 24
        data_date = cntHeader + 25

    End Enum

    'Index of AP11 Structure 
    Public Enum idxMsgAP11
        process_date = 1
        tpc_pw = cntHeader + 0
        dsc_pw = cntHeader + 1
        total_pw = cntHeader + 2
        tg1_pw = cntHeader + 3
        tg2_pw = cntHeader + 4
        trt1_pw = cntHeader + 5
        mtr_tns_volt = cntHeader + 6
        mtr_tns_pw = cntHeader + 7
        sp_volt = cntHeader + 8
        sp_pw = cntHeader + 9
        coke_volt = cntHeader + 10
        coke_pw = cntHeader + 11
        byprod_volt = cntHeader + 12
        byprod_pw = cntHeader + 13
        coke_total_pw = cntHeader + 14
        steel_volt = cntHeader + 15
        steel_pw = cntHeader + 16
        lime_volt = cntHeader + 17
        lime_pw = cntHeader + 18
        cc_volt = cntHeader + 19
        cc_pw = cntHeader + 20
        rh_volt = cntHeader + 21
        rh_pw = cntHeader + 22
        ac_volt = cntHeader + 23
        ac_pw = cntHeader + 24
        kr_volt = cntHeader + 25
        kr_pw = cntHeader + 26
        swt_volt = cntHeader + 27
        swt_pw = cntHeader + 28
        steel_total_pw = cntHeader + 29
        o2_volt = cntHeader + 30
        o2_pw = cntHeader + 31
        wt_volt = cntHeader + 32
        wt_pw = cntHeader + 33
        aps_volt = cntHeader + 34
        aps_pw = cntHeader + 35
        o2_total_pw = cntHeader + 36
        ph_volt = cntHeader + 37
        ph_pw = cntHeader + 38
        bf_volt = cntHeader + 39
        bf_pw = cntHeader + 40
        pci_volt = cntHeader + 41
        pci_pw = cntHeader + 42
        scrap_volt = cntHeader + 43
        scrap_pw = cntHeader + 44
        cwt_volt = cntHeader + 45
        cwt_pw = cntHeader + 46
        slab_wt_volt = cntHeader + 47
        slab_wt_pw = cntHeader + 48
        ph_total_pw = cntHeader + 49
        n33kv_volt = cntHeader + 50
        n33kv_pw = cntHeader + 51
        n9ka12_volt = cntHeader + 52
        n9ka12_pw = cntHeader + 53
        n9kb12_volt = cntHeader + 54
        n9kb12_pw = cntHeader + 55
        hr_total_pw = cntHeader + 56
        eaf_1101_volt = cntHeader + 57
        eaf_1101_pw = cntHeader + 58
        lf_1102_volt = cntHeader + 59
        lf_1102_pw = cntHeader + 60
        bkr_1207_volt = cntHeader + 61
        bkr_1207_pw = cntHeader + 62
        fmr_1208_volt = cntHeader + 63
        fmr_1208_pw = cntHeader + 64
        ss_1202_volt = cntHeader + 65
        ss_1202_pw = cntHeader + 66
        ss_1203_volt = cntHeader + 67
        ss_1203_pw = cntHeader + 68
        wt_1204_volt = cntHeader + 69
        wt_1204_pw = cntHeader + 70
        wt_1205_volt = cntHeader + 71
        wt_1205_pw = cntHeader + 72


    End Enum

    'Index of BP10 Structure 
    'Public Enum idxMsgBP10
    '    process_date = 1
    '    bv = cntHeader + 1
    '    bp = cntHeader + 2
    '    bt = cntHeader + 3
    '    moisture = cntHeader + 4
    '    ox_enrich_rate = cntHeader + 5
    '    air_rate = cntHeader + 6
    '    flame_temp = cntHeader + 7
    '    coal_inj_rate = cntHeader + 8
    '    scoke_rate = cntHeader + 9
    '    coke_rate = cntHeader + 10
    '    fuel_rate = cntHeader + 11
    '    tp = cntHeader + 12
    '    tt = cntHeader + 13
    '    co = cntHeader + 14
    '    co2 = cntHeader + 15
    '    h2 = cntHeader + 16
    '    uco = cntHeader + 17
    '    tgv = cntHeader + 18
    '    tusp = cntHeader + 19
    '    fgsp = cntHeader + 20
    '    solu = cntHeader + 21
    '    heat_flow_rate = cntHeader + 22
    '    spbv = cntHeader + 23
    '    sbhl = cntHeader + 24
    '    hmt = cntHeader + 25
    '    s1 = cntHeader + 26
    '    s = cntHeader + 27
    '    b2 = cntHeader + 28
    '    data_date = cntHeader + 29

    'End Enum
    Public Enum idxMsgBP10
        process_date = 1
        bfid = cntHeader + 0
        data_date = cntHeader + 1
        bv = cntHeader + 2
        bp = cntHeader + 3
        bt = cntHeader + 4
        moisture = cntHeader + 5
        ox_enrich_rate = cntHeader + 6
        air_rate = cntHeader + 7
        flame_temp = cntHeader + 8
        coal_inj_rate = cntHeader + 9
        scoke_rate = cntHeader + 10
        coke_rate = cntHeader + 11
        fuel_rate = cntHeader + 12
        tp = cntHeader + 13
        tt = cntHeader + 14
        co = cntHeader + 15
        co2 = cntHeader + 16
        h2 = cntHeader + 17
        uco = cntHeader + 18
        tgv = cntHeader + 19
        tusp = cntHeader + 20
        fgsp = cntHeader + 21
        solu = cntHeader + 22
        heat_flow_rate = cntHeader + 23
        spbv = cntHeader + 24
        sbhl = cntHeader + 25
        hmt = cntHeader + 26
        s1 = cntHeader + 27
        s = cntHeader + 28
        b2 = cntHeader + 29
    End Enum

    'Index of BP11 Structure 
    Public Enum idxMsgBP11
        process_date = 1
        trt = cntHeader + 1
        c1_level = cntHeader + 2
        c2_level = cntHeader + 3
        c3_level = cntHeader + 4
        c4_level = cntHeader + 5
        s1_level = cntHeader + 6
        s2_level = cntHeader + 7
        s3_level = cntHeader + 8
        s4_level = cntHeader + 9
        coke_wei = cntHeader + 10
        ore_wei = cntHeader + 11
        gear1_temp = cntHeader + 12
        gear2_temp = cntHeader + 13
        gear3_temp = cntHeader + 14
        airtight_east = cntHeader + 15
        airtight_west = cntHeader + 16
        coke_mode = cntHeader + 17
        ore_mode = cntHeader + 18
        t1oc_mode = cntHeader + 19
        cp_a_amt = cntHeader + 20
        cp_a_pd = cntHeader + 21
        cp_a_elec = cntHeader + 22
        cp_a_temp = cntHeader + 23
        cp_b_amt = cntHeader + 24
        cp_b_pd = cntHeader + 25
        cp_b_elec = cntHeader + 26
        cp_b_temp = cntHeader + 27
        cp_c_amt = cntHeader + 28
        cp_c_pd = cntHeader + 29
        cp_c_elec = cntHeader + 30
        cp_c_temp = cntHeader + 31
        reservoir_amt = cntHeader + 32
        CSi01_temp = cntHeader + 33
        CSi02_temp = cntHeader + 34
        CSi03_temp = cntHeader + 35
        CSi04_temp = cntHeader + 36
        CSo01_temp = cntHeader + 37
        CSo02_temp = cntHeader + 38
        CSo03_temp = cntHeader + 39
        CSo04_temp = cntHeader + 40
        ISi01_temp = cntHeader + 41
        ISi02_temp = cntHeader + 42
        ISi03_temp = cntHeader + 43
        ISi04_temp = cntHeader + 44
        ISo01_temp = cntHeader + 45
        ISo02_temp = cntHeader + 46
        ISo03_temp = cntHeader + 47
        ISo04_temp = cntHeader + 48
        TSi01_temp = cntHeader + 49
        TSi02_temp = cntHeader + 50
        TSi03_temp = cntHeader + 51
        TSi04_temp = cntHeader + 52
        TSo01_temp = cntHeader + 53
        TSo02_temp = cntHeader + 54
        TSo03_temp = cntHeader + 55
        TSo04_temp = cntHeader + 56
        bac_out_temp = cntHeader + 57
        bac_in_temp = cntHeader + 58
        headtank_wl = cntHeader + 59
        sw_basin_wl = cntHeader + 60
        rod_a_m = cntHeader + 61
        rod_b_m = cntHeader + 62
        rod_c_m = cntHeader + 63
        scoke_rate = cntHeader + 64
        coke_rate = cntHeader + 65
        coal_inj_rate = cntHeader + 66
        fuel_rate = cntHeader + 67
        sr = cntHeader + 68
        pr = cntHeader + 69
        orp = cntHeader + 70
        coke_base = cntHeader + 71
        slag_vol = cntHeader + 72
        sub_hl = cntHeader + 73
        tuyere_hl = cntHeader + 74
        total_hl = cntHeader + 75
        HW5_temp = cntHeader + 76
        HW4_temp = cntHeader + 77
        HW3_temp = cntHeader + 78
        HW2_temp = cntHeader + 79
        HW1_temp = cntHeader + 80
        HB4_temp = cntHeader + 81
        HB3_temp = cntHeader + 82
        HB2_temp = cntHeader + 83
        HB1_temp = cntHeader + 84
        UB2_temp = cntHeader + 85
        UB1_temp = cntHeader + 86
        co = cntHeader + 87
        co2 = cntHeader + 88
        h2 = cntHeader + 89
        eta_co = cntHeader + 90
        tp = cntHeader + 91
        tt = cntHeader + 92
        bv = cntHeader + 93
        bp = cntHeader + 94
        bt = cntHeader + 95
        humidity = cntHeader + 96
        ox_enrich_rate = cntHeader + 97
        coal_inj_rate_1 = cntHeader + 98
        flame_temp = cntHeader + 99
        air_per = cntHeader + 100
        blast_speed = cntHeader + 101
        flow_rate = cntHeader + 102
        spbv = cntHeader + 103
        n1bf_amt = cntHeader + 104
        n1bf_rate = cntHeader + 105
    End Enum

    'Index of BP12 Structure 
    Public Enum idxMsgBP12
        process_date = 1
        bfid = cntHeader + 0
        data_date = cntHeader + 1
        daily_amt = cntHeader + 2
        daily_amt_diff = cntHeader + 3
        daily_rate = cntHeader + 4
        month_amt = cntHeader + 5
        month_amt_diff = cntHeader + 6
        daily_bv = cntHeader + 7
        daily_bt = cntHeader + 8
        pci = cntHeader + 9
        hm_temp = cntHeader + 10
        hm_temp_sigma = cntHeader + 11
        ox_enrich_rate = cntHeader + 12
        fuel_rate = cntHeader + 13
        slag_rate = cntHeader + 14
        air_rate = cntHeader + 15
        hm_si = cntHeader + 16
        hm_si_sigma = cntHeader + 17
        bf_slag_b2 = cntHeader + 18
        hm_s = cntHeader + 19
        hm_s_sigma = cntHeader + 20

    End Enum

    'Index of BP13 Structure 
    Public Enum idxMsgBP13
        process_date = 1
        bfid = cntHeader + 0
        hole = cntHeader + 1
        tapno = cntHeader + 2
        tap_start_Date = cntHeader + 3
        tap_start_Time = cntHeader + 4
        slag_start_Date = cntHeader + 5
        slag_start_Time = cntHeader + 6
        tap_end_Date = cntHeader + 7
        tap_end_Time = cntHeader + 8
        tap_time = cntHeader + 9
        slag_time = cntHeader + 10
        gap_time = cntHeader + 11
        slag_index = cntHeader + 12
        tap_speed = cntHeader + 13
        hm_wt = cntHeader + 14
        slag_wt = cntHeader + 15
        hmt = cntHeader + 16
        hm_si = cntHeader + 17
        hm_mn = cntHeader + 18
        hm_p = cntHeader + 19
        hm_s = cntHeader + 20
        hm_ti = cntHeader + 21
        slag_cao = cntHeader + 22
        slag_mgo = cntHeader + 23
        slag_sio2 = cntHeader + 24
        slag_al2o3 = cntHeader + 25
        slag_b2 = cntHeader + 26
    End Enum

    'Index of CP10 Structure 
    Public Enum idxMsgCP10
        process_date = 1
        cokeid = cntHeader + 0
        data_date = cntHeader + 1
        daily_amt = cntHeader + 2
        daily_amt_diff = cntHeader + 3
        month_amt = cntHeader + 4
        month_amt_diff = cntHeader + 5
        stock_qty = cntHeader + 6
        coking_time = cntHeader + 7
        avg_size = cntHeader + 8

    End Enum

    'Index of CP11 Structure 
    Public Enum idxMsgCP11
        process_date = 1
        pushing_qty_1 = cntHeader + 0
        coal_tar_1 = cntHeader + 1
        nh3_1 = cntHeader + 2
        light_oil_1 = cntHeader + 3
        h2s_1 = cntHeader + 4
        naph_1 = cntHeader + 5
        water_1 = cntHeader + 6
        qi_1 = cntHeader + 7
        obd_1 = cntHeader + 8
        obe_1 = cntHeader + 9
        pgc_temp_n_1 = cntHeader + 10
        pgc_temp_m_1 = cntHeader + 11
        pgc_temp_a_1 = cntHeader + 12
        pushing_qty_2 = cntHeader + 13
        coal_tar_2 = cntHeader + 14
        nh3_2 = cntHeader + 15
        light_oil_2 = cntHeader + 16
        h2s_2 = cntHeader + 17
        naph_2 = cntHeader + 18
        water_2 = cntHeader + 19
        qi_2 = cntHeader + 20
        obd_2 = cntHeader + 21
        obe_2 = cntHeader + 22
        pgc_temp_n_2 = cntHeader + 23
        pgc_temp_m_2 = cntHeader + 24
        pgc_temp_a_2 = cntHeader + 25
        pushing_qty_sum = cntHeader + 26
        coal_tar_qty = cntHeader + 27
        coal_tar_exp = cntHeader + 28
        coal_tar_stock = cntHeader + 29
        coal_tar_stock_sum = cntHeader + 30
        light_oil_qty = cntHeader + 31
        light_oil_exp = cntHeader + 32
        light_oil_stock = cntHeader + 33
        light_oil_stock_sum = cntHeader + 34
        s_qty = cntHeader + 35
        s_exp = cntHeader + 36
        s_stock = cntHeader + 37
        s_stock_sum = cntHeader + 38
        oil_distillation = cntHeader + 39
        s_pure = cntHeader + 40
        data_date = cntHeader + 41


    End Enum

    'Index of CP12 Structure 
    Public Enum idxMsgCP12

        process_date = 1
        coke_level_1 = cntHeader + 0
        drop_qty_1 = cntHeader + 1
        pushing_qty_1 = cntHeader + 2
        achieving_rate_1 = cntHeader + 3
        utilization_1 = cntHeader + 4
        achieving_rate_helfhour_1 = cntHeader + 5
        drop_qty_month_1 = cntHeader + 6
        pushing_qty_month_1 = cntHeader + 7
        achieving_rate_month_1 = cntHeader + 8
        utilization_month_1 = cntHeader + 9
        achieving_rate_helfhour_month_1 = cntHeader + 10
        qf_qty_1 = cntHeader + 11
        elec_qty_1 = cntHeader + 12
        stm_qty_1 = cntHeader + 13
        elec_rate_1 = cntHeader + 14
        recv_coke_rate_1 = cntHeader + 15
        qf_qty_month_1 = cntHeader + 16
        elec_qty_month_1 = cntHeader + 17
        stm_qty_month_1 = cntHeader + 18
        elec_rate_month_1 = cntHeader + 19
        recv_coke_rate_month_1 = cntHeader + 20
        coke_level_2 = cntHeader + 21
        drop_qty_2 = cntHeader + 22
        pushing_qty_2 = cntHeader + 23
        achieving_rate_2 = cntHeader + 24
        utilization_2 = cntHeader + 25
        achieving_rate_helfhour_2 = cntHeader + 26
        drop_qty_month_2 = cntHeader + 27
        pushing_qty_month_2 = cntHeader + 28
        achieving_rate_month_2 = cntHeader + 29
        utilization_month_2 = cntHeader + 30
        achieving_rate_helfhour_month_2 = cntHeader + 31
        qf_qty_2 = cntHeader + 32
        elec_qty_2 = cntHeader + 33
        stm_qty_2 = cntHeader + 34
        elec_rate_2 = cntHeader + 35
        recv_coke_rate_2 = cntHeader + 36
        qf_qty_month_2 = cntHeader + 37
        elec_qty_month_2 = cntHeader + 38
        stm_qty_month_2 = cntHeader + 39
        elec_rate_month_2 = cntHeader + 40
        recv_coke_rate_month_2 = cntHeader + 41
        drop_qty_w21 = cntHeader + 42
        drop_qty_w221 = cntHeader + 43
        drop_qty_w222 = cntHeader + 44
        drop_qty_cdq = cntHeader + 45
        drop_qty_w262 = cntHeader + 46
        drop_qty_epa = cntHeader + 47
        drop_qty_etc = cntHeader + 48
        drop_qty_sum = cntHeader + 49
        cdq_drop = cntHeader + 50
        coke_use_qty = cntHeader + 51
        coke_diff = cntHeader + 52
        coke_stock = cntHeader + 53
        coke_prod_qty = cntHeader + 54
        coke_buy_qty = cntHeader + 55
        data_date = cntHeader + 56


    End Enum

    'Index of CP13 Structure 
    Public Enum idxMsgCP13

        process_date = 1
        first_in_cog_w = cntHeader + 0
        first_in_cog_p = cntHeader + 1
        first_out_cog_w = cntHeader + 2
        first_out_cog_p = cntHeader + 3
        exhaust_in_cog_w = cntHeader + 4
        exhaust_in_cog_p = cntHeader + 5
        exhaust_in_cog_f = cntHeader + 6
        exhaust_1x2201a_rs = cntHeader + 7
        exhaust_1x2201b_rs = cntHeader + 8
        nh3w_in_cog_p = cntHeader + 9
        nh3w_out_cog_w = cntHeader + 10
        nh3w_out_cog_p = cntHeader + 11
        oilw_out_cog_f = cntHeader + 12
        oilw_out_cog_w = cntHeader + 13
        oilw_out_cog_p = cntHeader + 14
        oilw_out_cog_h2sa = cntHeader + 15
        oilw_out_cog_nh3a = cntHeader + 16
        oilw_out_cog_oa = cntHeader + 17
        oilw_out_cog_ba = cntHeader + 18
        oilw_out_cog_mba = cntHeader + 19
        oilw_out_cog_xa = cntHeader + 20
        oilw_out_cog_ha = cntHeader + 21
        oilw_out_cog_o2a = cntHeader + 22
        oilw_out_cog_co2a = cntHeader + 23
        waterto34_cog_f = cntHeader + 24
        cws_out_f = cntHeader + 25
        stopy_1b3002a_f = cntHeader + 26
        stopy_1b3002b_f = cntHeader + 27
        stopy_1b3002a_w = cntHeader + 28
        stopy_1b3002b_w = cntHeader + 29
        nh3tobio_pha = cntHeader + 30
        nh3tonh3w_sa = cntHeader + 31
        nh3tonh3w_nh3a = cntHeader + 32
        nh3tonh3w_co2a = cntHeader + 33
        swtonh3w_h2sa = cntHeader + 34
        swtonh3w_nh3a = cntHeader + 35
        swtonh3w_co2a = cntHeader + 36
        nwtonw_h2sa = cntHeader + 37
        nwtonw_nh3a = cntHeader + 38
        nwtonw_co2a = cntHeader + 39
        botos_f = cntHeader + 40
        mps_qty = cntHeader + 41
        iw_qty = cntHeader + 42
        n_qty = cntHeader + 43
        wa_qty = cntHeader + 44
        ng_qty = cntHeader + 45
        py_1b3002a_cog_qty = cntHeader + 46
        py_1b3002b_cog_qty = cntHeader + 47
        cogtodown_qty = cntHeader + 48
        bleeder_qty = cntHeader + 49
    End Enum

    'Index of CP14_master Structure 
    Public Enum idxMsgCP14_master
        process_date = 1
        shift = cntHeader + 0
        amount = cntHeader + 1

    End Enum

    'Index of CP14_detail Structure 
    Public Enum idxMsgCP14_detail
        process_date = 1
        shift = cntHeader + 0
        amount = cntHeader + 1
        sn = cntHeader + 2
        number = cntHeader + 3
        condition = cntHeader + 4
        real_add_date = cntHeader + 5
        real_add_time = cntHeader + 6
        order_push_date = cntHeader + 7
        order_push_time = cntHeader + 8
        real_push_date = cntHeader + 9
        real_push_time = cntHeader + 10
        coke_time = cntHeader + 11
        push_car_number = cntHeader + 12
        weight = cntHeader + 13
        car_number = cntHeader + 14

    End Enum

    'Index of CP15_master Structure 
    Public Enum idxMsgCP15_master
        process_date = 1
        shift = cntHeader + 0
        amount = cntHeader + 1

    End Enum

    'Index of CP15_detail Structure 
    Public Enum idxMsgCP15_detail
        process_date = 1
        shift = cntHeader + 0
        amount = cntHeader + 1
        sn = cntHeader + 2
        number = cntHeader + 3
        condition = cntHeader + 4

    End Enum

    'Index of EP10 Structure 
    Public Enum idxMsgEP10
        process_date = 1
        n1blr_ms_flow = cntHeader + 0
        n1blr_ms_p = cntHeader + 1
        n1blr_ms_temp = cntHeader + 2
        n1blr_mixg = cntHeader + 3
        n1mixg_heat_val = cntHeader + 4
        n1blr_cog = cntHeader + 5
        n1blr_ng = cntHeader + 6
        n1blr_efficiency = cntHeader + 7
        n2blr_ms_flow = cntHeader + 8
        n2blr_ms_p = cntHeader + 9
        n2blr_ms_temp = cntHeader + 10
        n2blr_mixg = cntHeader + 11
        n2mixg_heat_val = cntHeader + 12
        n2blr_cog = cntHeader + 13
        n2blr_ng = cntHeader + 14
        n2blr_efficiency = cntHeader + 15
        n3blr_ms_flow = cntHeader + 16
        n3blr_ms_p = cntHeader + 17
        n3blr_ms_temp = cntHeader + 18
        n3blr_mixg = cntHeader + 19
        n3mixg_heat_val = cntHeader + 20
        n3blr_cog = cntHeader + 21
        n3blr_ng = cntHeader + 22
        n3blr_efficiency = cntHeader + 23
        n4blr_ms_flow = cntHeader + 24
        n4blr_ms_p = cntHeader + 25
        n4blr_ms_temp = cntHeader + 26
        n4blr_mixg = cntHeader + 27
        n4mixg_heat_val = cntHeader + 28
        n4blr_cog = cntHeader + 29
        n4blr_ng = cntHeader + 30
        n4blr_efficiency = cntHeader + 31
        n1tg_ms_flow = cntHeader + 32
        n1tg_ms_temp = cntHeader + 33
        n1tg_extr_flow = cntHeader + 34
        n1tg_extr_p = cntHeader + 35
        n1tg_extr_temp = cntHeader + 36
        n1tg_steam_out = cntHeader + 37
        n1tg_pw_gen = cntHeader + 38
        n1tg_heat_rate = cntHeader + 39
        n1tg_efficiency = cntHeader + 40
        n2tg_ms_flow = cntHeader + 41
        n2tg_ms_temp = cntHeader + 42
        n2tg_extr_flow = cntHeader + 43
        n2tg_extr_p = cntHeader + 44
        n2tg_extr_temp = cntHeader + 45
        n2tg_steam_out = cntHeader + 46
        n2tg_pw_gen = cntHeader + 47
        n2tg_heat_rate = cntHeader + 48
        n2tg_efficiency = cntHeader + 49
        n3tg_ms_flow = cntHeader + 50
        n3tg_ms_temp = cntHeader + 51
        n3tg_extr_flow = cntHeader + 52
        n3tg_extr_p = cntHeader + 53
        n3tg_extr_temp = cntHeader + 54
        n3tg_steam_out = cntHeader + 55
        n3tg_pw_gen = cntHeader + 56
        n3tg_heat_rate = cntHeader + 57
        n3tg_efficiency = cntHeader + 58
        n4tg_ms_flow = cntHeader + 59
        n4tg_ms_temp = cntHeader + 60
        n4tg_extr_flow = cntHeader + 61
        n4tg_extr_p = cntHeader + 62
        n4tg_extr_temp = cntHeader + 63
        n4tg_steam_out = cntHeader + 64
        n4tg_pw_gen = cntHeader + 65
        n4tg_heat_rate = cntHeader + 66
        n4tg_efficiency = cntHeader + 67
        n5tg_ms_flow = cntHeader + 68
        n5tg_ms_temp = cntHeader + 69
        n5tg_extr_flow = cntHeader + 70
        n5tg_extr_p = cntHeader + 71
        n5tg_extr_temp = cntHeader + 72
        n5tg_steam_out = cntHeader + 73
        n5tg_pw_gen = cntHeader + 74
        n5tg_heat_rate = cntHeader + 75
        n5tg_efficiency = cntHeader + 76
        total_efficiency = cntHeader + 77

    End Enum

    'Index of PP10 Structure 
    Public Enum idxMsgPP10
        process_date = 1
        mf_daily_amt = cntHeader + 0
        bof_daily_amt = cntHeader + 1
        daily_amt_diff = cntHeader + 2
        daily_scrap_add = cntHeader + 3

    End Enum

    'Index of QP11 Structure 
    Public Enum idxMsgQP11
        process_date = 1
        w40_p = cntHeader + 0
        w16_p = cntHeader + 1
        w40_f = cntHeader + 2
        w16_f = cntHeader + 3
        total_w_f = cntHeader + 4
        w_storage_wl = cntHeader + 5
        w_storage = cntHeader + 6
        tw_p = cntHeader + 7
        tw_f = cntHeader + 8

    End Enum

    'Index of QP12 Structure 
    Public Enum idxMsgQP12
        process_date = 1
        tw_month_amt = cntHeader + 0
        byprod_use = cntHeader + 1
        coke_use = cntHeader + 2
        sp_use = cntHeader + 3
        bf_use = cntHeader + 4
        fmr_use = cntHeader + 5
        lime_use = cntHeader + 6
        cc_use = cntHeader + 7
        bof_use = cntHeader + 8
        hr_use = cntHeader + 9
        ph_use = cntHeader + 10
        water_use = cntHeader + 11
        o2_use = cntHeader + 12
        etc_total = cntHeader + 13

    End Enum

    'Index of RP10 Structure 
    Public Enum idxMsgRP10
        process_date = 1
        data_date = cntHeader + 0
        turn = cntHeader + 1
        n1bf_sinter = cntHeader + 2
        n1bf_coke = cntHeader + 3
        n1bf_ore = cntHeader + 4
        n1bf_pellet = cntHeader + 4
    End Enum

    'Index of RP12 Structure 
    Public Enum idxMsgRP12
        process_date = 1
        mtr_id = cntHeader + 0
        db_qty = cntHeader + 1
        wb_qty = cntHeader + 2
        wb_use = cntHeader + 3
        ship = cntHeader + 4
        stack_addr = cntHeader + 5
        axis_start = cntHeader + 6
        axis_end = cntHeader + 7
        daily_qty = cntHeader + 8
        avg_qty = cntHeader + 9
        useup_date = cntHeader + 10
        data_date = cntHeader + 11
        mtr_name = 17 'table: i_master_mtrid,  column: mtr_name
    End Enum

    'Index of SP10 Structure 
    Public Enum idxMsgSP10
        process_date = 1
        data_date = cntHeader + 1
        daily_amt = cntHeader + 2
        month_amt = cntHeader + 3
        daily_amt_diff = cntHeader + 4
        month_amt_diff = cntHeader + 5
        stock_qty = cntHeader + 6
        stop_time = cntHeader + 7
        b2 = cntHeader + 8
        b2_sigma = cntHeader + 9
        feo = cntHeader + 10
        zn = cntHeader + 11
        iso = cntHeader + 12
        rdi = cntHeader + 13
        avg_size = cntHeader + 14
        sp_qty_10mm = cntHeader + 15
        sp_qty_5mm = cntHeader + 16

    End Enum

    'Index of SP11 Structure 
    Public Enum idxMsgSP11
        process_date = 1
        wf1_amt = cntHeader + 0
        wf2_amt = cntHeader + 1
        wf3_amt = cntHeader + 2
        wf4_amt = cntHeader + 3
        wf5_amt = cntHeader + 4
        wf6_amt = cntHeader + 5
        wf7_amt = cntHeader + 6
        wf8_amt = cntHeader + 7
        wf9_amt = cntHeader + 8
        wf10_amt = cntHeader + 9
        wf11_amt = cntHeader + 10
        wf12_amt = cntHeader + 11
        wf13_amt = cntHeader + 12
        wf14_amt = cntHeader + 13
        wf15_amt = cntHeader + 14
        wf16_amt = cntHeader + 15
        wf17_amt = cntHeader + 16
        wf18_amt = cntHeader + 17
        wf19_amt = cntHeader + 18
        wf20_amt = cntHeader + 19
        wf21_amt = cntHeader + 20
        wf22_amt = cntHeader + 21
        rf_level = cntHeader + 22
        rf_amt = cntHeader + 23
        sinter_mix = cntHeader + 24
        bof_slurry = cntHeader + 25
        dosing_water = cntHeader + 26
        moisture = cntHeader + 27
        raw_mix = cntHeader + 28
        pd_ep_opacity = cntHeader + 29
        hl_level = cntHeader + 30
        wg_ep_temp = cntHeader + 31
        wg_ep_cur1 = cntHeader + 32
        wg_ep_cur2 = cntHeader + 33
        wg_ep_cur3 = cntHeader + 34
        wg_ep_volt1 = cntHeader + 35
        wg_ep_volt2 = cntHeader + 36
        wg_ep_volt3 = cntHeader + 37
        wg_ep_suc_p = cntHeader + 38
        wg_ep_opacity = cntHeader + 39
        wg_fan_cur = cntHeader + 40
        wg_fan_damper = cntHeader + 41
        sm_ign_temp = cntHeader + 42
        sm_cog_flow = cntHeader + 43
        sm_comb_air = cntHeader + 44
        sm_speed = cntHeader + 45
        n12a_wb = cntHeader + 46
        n12b_wb = cntHeader + 47
        n13_wb = cntHeader + 48
        sp_prod = cntHeader + 49
        sp_qty_b2 = cntHeader + 50
        sp_qty_feo = cntHeader + 51
        sp_qty_sio2 = cntHeader + 52
        sp_qty_cao = cntHeader + 53
        sp_qty_tfe = cntHeader + 54
        sp_qty_ms = cntHeader + 55
        sp_qty_rdi = cntHeader + 56
        sp_qty_iso = cntHeader + 57
        sp_qty_5mm = cntHeader + 58
        sp_qty_10mm = cntHeader + 59
        sp_qty_zn = cntHeader + 60
        desox_effc = cntHeader + 61
        desox_inlet_temp = cntHeader + 62
        desox_outlet_sox = cntHeader + 63
        denox_effc = cntHeader + 64
        denox_inlet_temp = cntHeader + 65
        denox_inlet_nox = cntHeader + 66
        denox_outlet_nox = cntHeader + 67
        denox_nh3_slip = cntHeader + 68
        denox_nh3_use = cntHeader + 69
        cooler_fan_cur1 = cntHeader + 70
        cooler_fan_cur2 = cntHeader + 71
        cooler_fan_volt1 = cntHeader + 72
        cooler_fan_volt2 = cntHeader + 73
        sp_temp_cooler = cntHeader + 74
        cooler_speed = cntHeader + 75
        n1cool_fan_pw = cntHeader + 76
        n2cool_fan_pw = cntHeader + 77
        n1cool_fan_p = cntHeader + 78
        n2cool_fan_p = cntHeader + 79
        fs_amt = cntHeader + 80
        sm_bed_h = cntHeader + 81
        rf_rpm = cntHeader + 82
        comp_air_p = cntHeader + 83
        sm_ing_p = cntHeader + 84
        wg_temp = cntHeader + 85
        n1wb_temp = cntHeader + 86
        n2wb_temp = cntHeader + 87
        n3wb_temp = cntHeader + 88
        n14wb_temp = cntHeader + 89
        n1wb_suc_p = cntHeader + 90
        n2wb_suc_p = cntHeader + 91
        n3wb_suc_p = cntHeader + 92
        n12awb_suc_p = cntHeader + 93
        n12bwb_suc_p = cntHeader + 94
        n13wb_suc_p = cntHeader + 95
        n14wb_suc_p = cntHeader + 96
        bo_total = cntHeader + 97
        raw_mix_total = cntHeader + 98
        rd_winj = cntHeader + 99
        im_slurry = cntHeader + 100
        burnt_lime = cntHeader + 101
        catalyzer_pd1 = cntHeader + 102
        catalyzer_pd2 = cntHeader + 103
        catalyzer_pd3 = cntHeader + 104
        catalyzer_pd4 = cntHeader + 105
        wg_fan_temp = cntHeader + 106
        wg_fan_bear_temp1 = cntHeader + 107
        wg_fan_bear_temp2 = cntHeader + 108
        cog = cntHeader + 109
        pdf_cur = cntHeader + 110
        pdf_damper = cntHeader + 111
        pd_ep_gas_temp = cntHeader + 112
        pd_ep_sec_volt1 = cntHeader + 113
        pd_ep_sec_volt2 = cntHeader + 114
        pd_ep_sec_volt3 = cntHeader + 115
        pd_ep_sec_volt4 = cntHeader + 116
        pd_ep_sec_volt5 = cntHeader + 117
        pd_ep_sec_volt6 = cntHeader + 118
        pd_ep_sec_cur1 = cntHeader + 119
        pd_ep_sec_cur2 = cntHeader + 120
        pd_ep_sec_cur3 = cntHeader + 121
        pd_ep_sec_cur4 = cntHeader + 122
        pd_ep_sec_cur5 = cntHeader + 123
        pd_ep_sec_cur6 = cntHeader + 124
        pd_ep_opacity1 = cntHeader + 125
        nh3_slip = cntHeader + 126
        caoh2_use = cntHeader + 127
        b2 = cntHeader + 128
        cao = cntHeader + 129
        sio2 = cntHeader + 130
        mgo = cntHeader + 131
        feo = cntHeader + 132
        tfe = cntHeader + 133
        p50mm = cntHeader + 134
        p10mm = cntHeader + 135
        m5mm = cntHeader + 136
        ms = cntHeader + 137
        rdi = cntHeader + 138
        iso63mm = cntHeader + 139

    End Enum

    'Index of SP12 Structure 
    Public Enum idxMsgSP12
        process_date = 1
        spid = cntHeader + 0
        amt = cntHeader + 1
        raw_amt = cntHeader + 2
        blending_amt = cntHeader + 3
        lime_lvl = cntHeader + 4
        lime_rate = cntHeader + 5
        sp_cur = cntHeader + 6
        sp_speed = cntHeader + 7
        hr_cur = cntHeader + 8
        mix = cntHeader + 9
        height = cntHeader + 10
        temp = cntHeader + 11
        cog = cntHeader + 12
        sp_5mm = cntHeader + 13
        ms = cntHeader + 14
        epi_temp = cntHeader + 15
        epo_temp = cntHeader + 16
        ep_cur_a1 = cntHeader + 17
        ep_cur_b1 = cntHeader + 18
        ep_cur_a = cntHeader + 19
        ep_cur_b = cntHeader + 20
        wgf = cntHeader + 21
        pdf = cntHeader + 22
        ep_op_l = cntHeader + 23
        ep_op_s = cntHeader + 24
        spare = cntHeader + 25
    End Enum
    'Index of WK1P Structure 
    Public Enum idxMsgWK1P
        process_date = 1
        data_date = cntHeader + 0
        bf1_daily_amt = cntHeader + 1
        bf2_daily_amt = cntHeader + 2
        bf_daily_amt_sum = cntHeader + 3
        bf_daily_amt_diff = cntHeader + 4
        bf1_daily_amt_rate = cntHeader + 5
        bf2_daily_amt_rate = cntHeader + 6
        bf_daily_amt_rate_avg = cntHeader + 7
        bf1_month_amt = cntHeader + 8
        bf2_month_amt = cntHeader + 9
        bf_month_amt_sum = cntHeader + 10
        bf_month_amt_diff = cntHeader + 11
        bf1_pci = cntHeader + 12
        bf2_pci = cntHeader + 13
        bf_pci_avg = cntHeader + 14
        bf1_enrich_rate = cntHeader + 15
        bf2_enrich_rate = cntHeader + 16
        bf_enrich_rate_avg = cntHeader + 17
        coke1_daily_amt = cntHeader + 18
        coke2_daily_amt = cntHeader + 19
        coke_daily_amt_sum = cntHeader + 20
        coke_daily_amt_diff = cntHeader + 21
        coke1_month_amt = cntHeader + 22
        coke2_month_amt = cntHeader + 23
        coke_month_amt_sum = cntHeader + 24
        coke_month_amt_diff = cntHeader + 25
        coke1_stock_qty = cntHeader + 26
        coke2_stock_qty = cntHeader + 27
        coke_stock_sum = cntHeader + 28
        coke1_coking_time = cntHeader + 29
        coke2_coking_time = cntHeader + 30
        coke1_avg_size = cntHeader + 31
        coke2_avg_size = cntHeader + 32
        sp1_daily_amt = cntHeader + 33
        sp2_daily_amt = cntHeader + 34
        sp_daily_amt_sum = cntHeader + 35
        sp_daily_amt_diff = cntHeader + 36
        sp1_month_amt = cntHeader + 37
        sp2_month_amt = cntHeader + 38
        sp_month_amt_sum = cntHeader + 39
        sp_month_amt_diff = cntHeader + 40
        sp1_stock_qty = cntHeader + 41
        sp2_stock_qty = cntHeader + 42
        sp_stock_sum = cntHeader + 43
        eaf_daily_realamt = cntHeader + 44
        bof_daily_realamt = cntHeader + 45
        steel_daily_amt_diff = cntHeader + 46
        steel_daily_scrap = cntHeader + 47
        eaf_month_realamt = cntHeader + 48
        bof_month_realamt = cntHeader + 49
        steel_month_amt_diff = cntHeader + 50
        steel_month_scrap = cntHeader + 51
    End Enum

    ''Index of BP20_util Structure 
    'Public Enum idxMsgBP20_util
    '    process_date = 0
    '    fx113 = 1
    '    fx201 = 2
    '    fx212 = 3
    '    fx317 = 4
    '    fx318 = 5
    '    fx336 = 6
    '    fx335 = 7
    '    fx516 = 8
    '    fx528 = 9
    '    fx631 = 10
    '    fx632 = 11
    '    fx716 = 12
    '    fx715 = 13
    '    fx814 = 14
    '    fx912 = 15
    'End Enum

    ''Index of CP20_util Structure 
    'Public Enum idxMsgCP20_util
    '    process_date = 0
    '    fx101 = 1
    '    fx106 = 2
    '    fx109 = 3
    '    fx312 = 4
    '    fx322 = 5
    '    fx328 = 6
    '    fx511 = 7
    '    fx512 = 8
    '    fx712 = 9
    '    fx718 = 10
    '    fx811 = 11
    '    fx812 = 12
    '    fx911 = 13
    'End Enum

    ''Index of FP20_util Structure 
    'Public Enum idxMsgFP20_util
    '    process_date = 0
    '    fx111 = 1
    '    fx314 = 2
    '    fx315 = 3
    '    fx316 = 4
    '    fx360 = 5
    '    fx514 = 6
    '    fx515 = 7
    '    fx529 = 8
    '    fx615 = 9
    '    fx621 = 10
    '    fx622 = 11
    '    fx713 = 12
    '    fx714 = 13
    '    fx817 = 14
    '    fx917 = 15
    'End Enum

    ''Index of OP20_util Structure 
    'Public Enum idxMsgOP20_util
    '    process_date = 0
    '    fx303 = 1
    '    fx304 = 2
    '    fx501 = 3
    '    fx502 = 4
    '    fx503 = 5
    '    fx601 = 6
    '    fx602 = 7
    '    fx603 = 8
    '    fx604 = 9
    '    fx605 = 10
    '    fx606 = 11
    '    fx671 = 12
    '    fx672 = 13
    '    fx673 = 14
    '    fx332 = 15
    '    fx802 = 16
    'End Enum

    ''Index of PP20_util Structure 
    'Public Enum idxMsgPP20_util
    '    process_date = 0
    '    fx319 = 1
    '    fx531 = 2
    'End Enum

    ''Index of PP21_util Structure 
    'Public Enum idxMsgPP21_util
    '    process_date = 0
    '    fx117 = 1
    '    fx115 = 2
    '    fx118 = 3
    '    fx321 = 4
    '    fx521 = 5
    '    fx681 = 6
    '    fx711 = 7
    '    fx717 = 8
    '    fx721 = 9
    'End Enum

    ''Index of PP22_util Structure 
    'Public Enum idxMsgPP22_util
    '    process_date = 0
    '    fx119 = 1
    '    fx337 = 2
    '    fx532 = 3
    '    fx611 = 4
    '    fx682 = 5
    '    fx724 = 6
    '    fx816 = 7
    'End Enum

    ''Index of RP20_util Structure 
    'Public Enum idxMsgRP20_util
    '    process_date = 0
    '    fx311 = 1
    '    fx338 = 2
    'End Enum

    ''Index of SP20_util Structure 
    'Public Enum idxMsgSP20_util
    '    process_date = 0
    '    fx112 = 1
    '    fx132 = 2  '原fx125 
    '    fx313 = 3
    '    fx333 = 4
    '    fx513 = 5
    '    fx530 = 6
    '    fx813 = 7
    '    fx818 = 8
    'End Enum

    ''Index of SP75_util Structure 
    'Public Enum idxMsgSP75_util
    '    process_date = 0
    '    ccid = 1
    '    fx120 = 2
    '    fx325 = 3
    '    fx361 = 4
    '    fx612 = 5
    '    fx524 = 6
    '    fx683 = 7
    '    fx719 = 8
    'End Enum

    ''Index of UP20_util Structure 
    'Public Enum idxMsgUP20_util
    '    process_date = 0
    '    ft103 = 1
    '    ft105 = 2
    '    ft104 = 3
    '    ft211 = 4
    '    ft114 = 5
    '    ft202 = 6
    '    ft116 = 7
    '    ft320 = 8
    '    ft519 = 9
    '    ft815 = 10
    '    ft203 = 11
    '    ft520 = 12
    '    ft610 = 13
    '    ft121 = 14
    '    ft326 = 15
    '    fx340 = 16 '原fx328
    '    fx329 = 17
    '    fx330 = 18
    '    fx331 = 19
    '    fx637 = 20
    '    fx914 = 21
    '    fx915 = 22
    '    ft123 = 23
    '    ft124 = 24
    '    ft125 = 25
    '    ft126 = 26
    '    ft206 = 27
    '    ft213 = 28
    '    ft214 = 29
    '    ft219 = 30
    '    fx327 = 31
    '    ft334 = 32
    '    ft518 = 33
    '    fx525 = 34
    '    ft620 = 35
    '    ft725 = 36
    '    ft554 = 37
    '    ft704 = 38
    '    ft722 = 39
    '    ft723 = 40
    '    fx801 = 41
    '    ft901 = 42
    '    ft301 = 43
    '    fepa01 = 44
    '    ft302 = 45
    '    ft607 = 46
    '    ft701 = 47
    '    ft702 = 48
    '    ft703 = 49
    '    ft770 = 50
    '    ft916 = 51
    '    ft357 = 52
    '    ft358 = 53
    '    ft771 = 54
    '    ft627 = 55
    '    fepa04 = 56
    '    fepam01 = 57
    '    ft552 = 58
    '    ft624 = 59
    '    ft693 = 60
    '    ft752 = 61
    '    fepa03 = 62
    '    ft127 = 63
    '    fepa05 = 64
    '    fepa06 = 65
    '    ft363 = 66
    '    ft625 = 67
    '    ft751 = 68
    '    ft691 = 69
    '    ft821 = 70

    'End Enum

    'i_pmis_bp20_day
    Public Enum idxMsgBP20_day
        process_date = 0
        fx113 = 1
        fx201 = 2
        fx212 = 3
        fx317 = 4
        fx318 = 5
        fx336 = 6
        fx335 = 7
        fx516 = 8
        fx528 = 9
        fx631 = 10
        fx632 = 11
        fx716 = 12
        fx715 = 13
        fx814 = 14
        fx912 = 15
    End Enum

    'i_pmis_cp20_day
    Public Enum idxMsgCP20_day
        process_date = 0
        fx101 = 1
        fx106 = 2
        fx109 = 3
        fx312 = 4
        fx322 = 5
        fx328 = 6
        fx511 = 7
        fx512 = 8
        fx712 = 9
        fx718 = 10
        fx811 = 11
        fx812 = 12
        fx911 = 13
    End Enum

    'i_pmis_fp20_day
    Public Enum idxMsgFP20_day
        process_date = 0
        fx111 = 1
        fx314 = 2
        fx315 = 3
        fx316 = 4
        fx360 = 5
        fx514 = 6
        fx515 = 7
        fx529 = 8
        fx615 = 9
        fx621 = 10
        fx622 = 11
        fx713 = 12
        fx714 = 13
        fx817 = 14
        fx917 = 15
    End Enum

    'i_pmis_op20_day
    Public Enum idxMsgOP20_day
        process_date = 0
        fx303 = 1
        fx304 = 2
        fx501 = 3
        fx502 = 4
        fx503 = 5
        fx601 = 6
        fx602 = 7
        fx603 = 8
        fx604 = 9
        fx605 = 10
        fx606 = 11
        fx671 = 12
        fx672 = 13
        fx673 = 14
        fx332 = 15
        fx802 = 16
    End Enum

    'i_pmis_pp20_day
    Public Enum idxMsgPP20_day
        process_date = 0
        fx319 = 1
        fx531 = 2
    End Enum

    'i_pmis_pp21_day
    Public Enum idxMsgPP21_day
        process_date = 0
        fx117 = 1
        fx115 = 2
        fx118 = 3
        ft203 = 4
        fx321 = 5
        ft520 = 6
        fx521 = 7
        ft610 = 8
        fx681 = 9
        fx711 = 10
        fx717 = 11
        fx721 = 12
    End Enum

    'i_pmis_pp22_day
    Public Enum idxMsgPP22_day
        process_date = 0
        fx119 = 1
        fx337 = 2
        fx532 = 3
        fx611 = 4
        fx682 = 5
        fx724 = 6
        fx816 = 7
    End Enum

    'i_pmis_qp20_day
    'Public Enum idxMsgQP20_day
    '    process_date = 0
    '    fx328 = 1
    '    fx329 = 2
    '    fx330 = 3
    '    fx331 = 4
    '    fx637 = 5
    '    fx914 = 6
    '    fx915 = 7
    'End Enum

    'i_pmis_rp20_day
    Public Enum idxMsgRP20_day
        process_date = 0
        fx311 = 1
        fx338 = 2
    End Enum

    'i_pmis_sp20_day
    Public Enum idxMsgSP20_day
        process_date = 0
        fx112 = 1
        fx132 = 2
        fx313 = 3
        fx333 = 4
        fx513 = 5
        fx530 = 6
        fx813 = 7
        fx818 = 8
    End Enum

    'i_pmis_sp75_day
    Public Enum idxMsgSP75_day
        process_date = 0
        ccid = 1
        fx120 = 2
        fx325 = 3
        fx361 = 4
        fx612 = 5
        fx524 = 6
        fx683 = 7
        fx719 = 8
    End Enum

    'i_pmis_up20_day
    Public Enum idxMsgUP20_day
        process_date = 0
        ft103 = 1
        ft105 = 2
        ft104 = 3
        ft211 = 4
        ft114 = 5
        ft202 = 6
        ft116 = 7
        ft320 = 8
        ft519 = 9
        ft815 = 10
        ft203 = 11
        ft520 = 12
        ft610 = 13
        ft121 = 14
        ft326 = 15
        fx340 = 16
        fx329 = 17
        fx330 = 18
        fx331 = 19
        fx637 = 20
        fx914 = 21
        fx915 = 22
        ft123 = 23
        ft124 = 24
        ft125 = 25
        ft126 = 26
        ft206 = 27
        ft213 = 28
        ft214 = 29
        ft219 = 30
        fx327 = 31
        ft334 = 32
        ft518 = 33
        fx525 = 34
        ft620 = 35
        ft725 = 36
        ft554 = 37
        ft704 = 38
        ft722 = 39
        ft723 = 40
        fx801 = 41
        ft901 = 42
        ft301 = 43
        fepa01 = 44
        ft302 = 45
        ft607 = 46
        ft701 = 47
        ft702 = 48
        ft703 = 49
        ft770 = 50
        ft916 = 51
        ft357 = 52
        ft358 = 53
        ft771 = 54
        ft627 = 55
        fepa04 = 56
        fepam01 = 57
        ft552 = 58
        ft624 = 59
        ft693 = 60
        ft752 = 61
        fepa03 = 62
        ft127 = 63
        fepa05 = 64
        fepa06 = 65
        ft363 = 66
        ft625 = 67
        ft751 = 68
        ft691 = 69
        ft821 = 70
        ft551 = 71
    End Enum

    'Index of MS01 Structure 
    Public Enum idxMsgMS01
        process_date = 1
        krno = cntHeader + 0
        heatno = cntHeader + 1
        status = cntHeader + 2
        impeller_lift = cntHeader + 3
        ld_atime = cntHeader + 4
        treat_stime = cntHeader + 5
        treat_etime = cntHeader + 6
        ld_dtime = cntHeader + 7
        ptime = cntHeader + 8
        mt_consum = cntHeader + 9
        targets = cntHeader + 10
        sb_treat = cntHeader + 11
        sa_treat = cntHeader + 12
    End Enum

    'Index of MS03 Structure 
    Public Enum idxMsgMS03
        process_date = 1
        bofno = cntHeader + 0
        turn = cntHeader + 1
        shiftno = cntHeader + 2
        name = cntHeader + 3
        blow_count = cntHeader + 4
        avg_blow_t = cntHeader + 5
        avg_free_o2 = cntHeader + 6
        avg_ldg_r = cntHeader + 7
        vender_1 = cntHeader + 8
        coat_count = cntHeader + 9
        out_count = cntHeader + 10
        vender_2 = cntHeader + 11
        location = cntHeader + 12
        count = cntHeader + 13
        rstime = cntHeader + 14
        rptime = cntHeader + 15
        co_wgas = cntHeader + 16
        co2_wgas = cntHeader + 17
        o2_wgas = cntHeader + 18
    End Enum

    'Index of MS04 Structure 
    Public Enum idxMsgMS04
        processdate = 1
        injsno = cntHeader + 0
        heatno = cntHeader + 1
        starttime = cntHeader + 2
        endtime = cntHeader + 3
        geno = cntHeader + 4
        spec = cntHeader + 5
        lanceflow = cntHeader + 6
        powderwt = cntHeader + 7
        time1 = cntHeader + 8
        temperature1 = cntHeader + 9
        time2 = cntHeader + 10
        temperature2 = cntHeader + 11
        time3 = cntHeader + 12
        temperature3 = cntHeader + 13
        time4 = cntHeader + 14
        temperature4 = cntHeader + 15
        time5 = cntHeader + 16
        temperature5 = cntHeader + 17
        n_count = cntHeader + 18
        m_count = cntHeader + 19
        a_count = cntHeader + 20
    End Enum

    'Index of MS05 Structure 
    Public Enum idxMsgMS05
        process_date = 1
        rhsno = cntHeader + 0
        heatno = cntHeader + 1
        stime = cntHeader + 2
        etime = cntHeader + 3
        geno = cntHeader + 4
        spec = cntHeader + 5
        vacumn = cntHeader + 6
        sk_flow = cntHeader + 7
        v_temp = cntHeader + 8
        time_1 = cntHeader + 9
        temperature_1 = cntHeader + 10
        free_o2_1 = cntHeader + 11
        time_2 = cntHeader + 12
        temperature_2 = cntHeader + 13
        free_o2_2 = cntHeader + 14
        time_3 = cntHeader + 15
        temperature_3 = cntHeader + 16
        free_o2_3 = cntHeader + 17
        time_4 = cntHeader + 18
        temperature_4 = cntHeader + 19
        free_o2_4 = cntHeader + 20
        time_5 = cntHeader + 21
        temperature_5 = cntHeader + 22
        free_o2_5 = cntHeader + 23
        time_6 = cntHeader + 24
        temperature_6 = cntHeader + 25
        free_o2_6 = cntHeader + 26
        time_7 = cntHeader + 27
        temperature_7 = cntHeader + 28
        free_o2_7 = cntHeader + 29
        n_count = cntHeader + 30
        m_count = cntHeader + 31
        a_count = cntHeader + 32
    End Enum

    'Index of MS06 Structure 
    Public Enum idxMsgMS06
        process_date = 1
        hmno = cntHeader + 0
        status = cntHeader + 1
        alert = cntHeader + 2
        wall_count = cntHeader + 3
        wall_vender = cntHeader + 4
        bottom_count = cntHeader + 5
        bottom_vender = cntHeader + 6
        reladle_count = cntHeader + 7
        offline = cntHeader + 8

    End Enum

    'Index of MS08 Structure 
    Public Enum idxMsgMS08
        process_date = 1
        stnsno = cntHeader + 0
        heatno = cntHeader + 1
        stime = cntHeader + 2
        etime = cntHeader + 3
        geno = cntHeader + 4
        spec = cntHeader + 5
        ld_flow = cntHeader + 6
        time_1 = cntHeader + 7
        temperature_1 = cntHeader + 8
        free_o2_1 = cntHeader + 9
        time_2 = cntHeader + 10
        temperature_2 = cntHeader + 11
        free_o2_2 = cntHeader + 12
        time_3 = cntHeader + 13
        temperature_3 = cntHeader + 14
        free_o2_3 = cntHeader + 15
        time_4 = cntHeader + 16
        temperature_4 = cntHeader + 17
        free_o2_4 = cntHeader + 18
        time_5 = cntHeader + 19
        temperature_5 = cntHeader + 20
        free_o2_5 = cntHeader + 21
        n_count = cntHeader + 22
        m_count = cntHeader + 23
        a_count = cntHeader + 24

    End Enum

    'Index of MS09 Structure 
    Public Enum idxMsgMS09
        'process_date = 1
        'kr1_ld_no = cntHeader + 0
        'kr1_ld_sts = cntHeader + 1
        'kr1_hm_aw = cntHeader + 2
        'kr2_ld_no = cntHeader + 3
        'kr2_ld_sts = cntHeader + 4
        'kr2_hm_aw = cntHeader + 5
        'tr1_s_ld_no = cntHeader + 6
        'tr1_s_ld_sts = cntHeader + 7
        'tr1_s_hm_aw = cntHeader + 8
        'tr1_n_ld_no = cntHeader + 9
        'tr1_n_ld_sts = cntHeader + 10
        'tr1_n_hm_aw = cntHeader + 11
        'tr2_s_ld_no = cntHeader + 12
        'tr2_s_ld_sts = cntHeader + 13
        'tr2_s_hm_aw = cntHeader + 14
        'tr2_n_ld_no = cntHeader + 15
        'tr2_n_ld_sts = cntHeader + 16
        'tr2_n_hm_aw = cntHeader + 17
        'tl11_ld_no = cntHeader + 18
        'tl11_ld_sts = cntHeader + 19
        'tl11_hm_aw = cntHeader + 20
        'tl12_ld_no = cntHeader + 21
        'tl12_ld_sts = cntHeader + 22
        'tl12_hm_aw = cntHeader + 23
        'tl13_ld_no = cntHeader + 24
        'tl13_ld_sts = cntHeader + 25
        'tl13_hm_aw = cntHeader + 26
        'tl14_ld_no = cntHeader + 27
        'tl14_ld_sts = cntHeader + 28
        'tl14_hm_aw = cntHeader + 29
        'tl15_ld_no = cntHeader + 30
        'tl15_ld_sts = cntHeader + 31
        'tl15_hm_aw = cntHeader + 32
        'tl16_ld_no = cntHeader + 33
        'tl16_ld_sts = cntHeader + 34
        'tl16_hm_aw = cntHeader + 35
        'ec11_ld_no = cntHeader + 36
        'ec11_ld_sts = cntHeader + 37
        'ec11_hm_aw = cntHeader + 38
        'ec12_ld_no = cntHeader + 39
        'ec12_ld_sts = cntHeader + 40
        'ec12_hm_aw = cntHeader + 41
        'ec13_ld_no = cntHeader + 42
        'ec13_ld_sts = cntHeader + 43
        'ec13_hm_aw = cntHeader + 44
        'ec14_ld_no = cntHeader + 45
        'ec14_ld_sts = cntHeader + 46
        'ec14_hm_aw = cntHeader + 47
        'ec15_ld_no = cntHeader + 48
        'ec15_ld_sts = cntHeader + 49
        'ec15_hm_aw = cntHeader + 50
        'ec16_ld_no = cntHeader + 51
        'ec16_ld_sts = cntHeader + 52
        'ec16_hm_aw = cntHeader + 53
        'ps01_ld_no = cntHeader + 54
        'ps01_ld_sts = cntHeader + 55
        'ps01_hm_aw = cntHeader + 56
        'ps02_ld_no = cntHeader + 57
        'ps02_ld_sts = cntHeader + 58
        'ps02_hm_aw = cntHeader + 59
        'ps03_ld_no = cntHeader + 60
        'ps03_ld_sts = cntHeader + 61
        'ps03_hm_aw = cntHeader + 62
        'ps04_ld_no = cntHeader + 63
        'ps04_ld_sts = cntHeader + 64
        'ps04_hm_aw = cntHeader + 65
        'bs01_ld_no = cntHeader + 66
        'bs01_ld_sts = cntHeader + 67
        'bs01_hm_aw = cntHeader + 68
        'bs02_ld_no = cntHeader + 69
        'bs02_ld_sts = cntHeader + 70
        'bs02_hm_aw = cntHeader + 71
        'bs03_ld_no = cntHeader + 72
        'bs03_ld_sts = cntHeader + 73
        'bs03_hm_aw = cntHeader + 74
        'dryer1_ld_no = cntHeader + 75
        'dryer1_ld_sts = cntHeader + 76
        'dryer1_hm_aw = cntHeader + 77
        'dryer2_ld_no = cntHeader + 78
        'dryer2_ld_sts = cntHeader + 79
        'dryer2_hm_aw = cntHeader + 80
        'dryer3_ld_no = cntHeader + 81
        'dryer3_ld_sts = cntHeader + 82
        'dryer3_hm_aw = cntHeader + 83
        'pigm_ld_no = cntHeader + 84
        'pigm_ld_sts = cntHeader + 85
        'pigm_hm_aw = cntHeader + 86
        'dump_ld_no = cntHeader + 87
        'dump_ld_sts = cntHeader + 88
        'dump_hm_aw = cntHeader + 89
        'rlds1_ld_no = cntHeader + 90
        'rlds1_ld_sts = cntHeader + 91
        'rlds1_hm_aw = cntHeader + 92
        'eca1_ld_no = cntHeader + 93
        'eca1_ld_sts = cntHeader + 94
        'eca1_hm_aw = cntHeader + 95
        'wbof1_ld_no = cntHeader + 96
        'wbof1_ld_sts = cntHeader + 97
        'wbof1_hm_aw = cntHeader + 98
        'weaf_ld_no = cntHeader + 99
        'weaf_ld_sts = cntHeader + 100
        'weaf_hm_aw = cntHeader + 101
        'rep_bof_ld = cntHeader + 102
        'rep_eaf_ld = cntHeader + 103
        process_date = 1
        kr1_ld_no = cntHeader + 0
        kr1_ld_sts = cntHeader + 1
        kr1_hm_aw = cntHeader + 2
        kr2_ld_no = cntHeader + 3
        kr2_ld_sts = cntHeader + 4
        kr2_hm_aw = cntHeader + 5
        kr3_ld_no = cntHeader + 6
        kr3_ld_sts = cntHeader + 7
        kr3_hm_aw = cntHeader + 8
        kr4_ld_no = cntHeader + 9
        kr4_ld_sts = cntHeader + 10
        kr4_hm_aw = cntHeader + 11
        tr1_s_ld_no = cntHeader + 12
        tr1_s_ld_sts = cntHeader + 13
        tr1_s_hm_aw = cntHeader + 14
        tr1_n_ld_no = cntHeader + 15
        tr1_n_ld_sts = cntHeader + 16
        tr1_n_hm_aw = cntHeader + 17
        tr2_s_ld_no = cntHeader + 18
        tr2_s_ld_sts = cntHeader + 19
        tr2_s_hm_aw = cntHeader + 20
        tr2_n_ld_no = cntHeader + 21
        tr2_n_ld_sts = cntHeader + 22
        tr2_n_hm_aw = cntHeader + 23
        tr3_s_ld_no = cntHeader + 24
        tr3_s_ld_sts = cntHeader + 25
        tr3_s_hm_aw = cntHeader + 26
        tr3_n_ld_no = cntHeader + 27
        tr3_n_ld_sts = cntHeader + 28
        tr3_n_hm_aw = cntHeader + 29
        tr4_s_ld_no = cntHeader + 30
        tr4_s_ld_sts = cntHeader + 31
        tr4_s_hm_aw = cntHeader + 32
        tr4_n_ld_no = cntHeader + 33
        tr4_n_ld_sts = cntHeader + 34
        tr4_n_hm_aw = cntHeader + 35
        tl11_ld_no = cntHeader + 36
        tl11_ld_sts = cntHeader + 37
        tl11_hm_aw = cntHeader + 38
        tl12_ld_no = cntHeader + 39
        tl12_ld_sts = cntHeader + 40
        tl12_hm_aw = cntHeader + 41
        tl13_ld_no = cntHeader + 42
        tl13_ld_sts = cntHeader + 43
        tl13_hm_aw = cntHeader + 44
        tl14_ld_no = cntHeader + 45
        tl14_ld_sts = cntHeader + 46
        tl14_hm_aw = cntHeader + 47
        tl15_ld_no = cntHeader + 48
        tl15_ld_sts = cntHeader + 49
        tl15_hm_aw = cntHeader + 50
        tl16_ld_no = cntHeader + 51
        tl16_ld_sts = cntHeader + 52
        tl16_hm_aw = cntHeader + 53
        tl21_ld_no = cntHeader + 54
        tl21_ld_sts = cntHeader + 55
        tl21_hm_aw = cntHeader + 56
        tl22_ld_no = cntHeader + 57
        tl22_ld_sts = cntHeader + 58
        tl22_hm_aw = cntHeader + 59
        tl23_ld_no = cntHeader + 60
        tl23_ld_sts = cntHeader + 61
        tl23_hm_aw = cntHeader + 62
        tl24_ld_no = cntHeader + 63
        tl24_ld_sts = cntHeader + 64
        tl24_hm_aw = cntHeader + 65
        tl25_ld_no = cntHeader + 66
        tl25_ld_sts = cntHeader + 67
        tl25_hm_aw = cntHeader + 68
        tl26_ld_no = cntHeader + 69
        tl26_ld_sts = cntHeader + 70
        tl26_hm_aw = cntHeader + 71
        ec11_ld_no = cntHeader + 72
        ec11_ld_sts = cntHeader + 73
        ec11_hm_aw = cntHeader + 74
        ec12_ld_no = cntHeader + 75
        ec12_ld_sts = cntHeader + 76
        ec12_hm_aw = cntHeader + 77
        ec13_ld_no = cntHeader + 78
        ec13_ld_sts = cntHeader + 79
        ec13_hm_aw = cntHeader + 80
        ec14_ld_no = cntHeader + 81
        ec14_ld_sts = cntHeader + 82
        ec14_hm_aw = cntHeader + 83
        ec15_ld_no = cntHeader + 84
        ec15_ld_sts = cntHeader + 85
        ec15_hm_aw = cntHeader + 86
        ec16_ld_no = cntHeader + 87
        ec16_ld_sts = cntHeader + 88
        ec16_hm_aw = cntHeader + 89
        ec21_ld_no = cntHeader + 90
        ec21_ld_sts = cntHeader + 91
        ec21_hm_aw = cntHeader + 92
        ec22_ld_no = cntHeader + 93
        ec22_ld_sts = cntHeader + 94
        ec22_hm_aw = cntHeader + 95
        ec23_ld_no = cntHeader + 96
        ec23_ld_sts = cntHeader + 97
        ec23_hm_aw = cntHeader + 98
        ec24_ld_no = cntHeader + 99
        ec24_ld_sts = cntHeader + 100
        ec24_hm_aw = cntHeader + 101
        ec25_ld_no = cntHeader + 102
        ec25_ld_sts = cntHeader + 103
        ec25_hm_aw = cntHeader + 104
        ec26_ld_no = cntHeader + 105
        ec26_ld_sts = cntHeader + 106
        ec26_hm_aw = cntHeader + 107
        bs01_ld_no = cntHeader + 108
        bs01_ld_sts = cntHeader + 109
        bs01_hm_aw = cntHeader + 110
        bs02_ld_no = cntHeader + 111
        bs02_ld_sts = cntHeader + 112
        bs02_hm_aw = cntHeader + 113
        bs03_ld_no = cntHeader + 114
        bs03_ld_sts = cntHeader + 115
        bs03_hm_aw = cntHeader + 116
        bs04_ld_no = cntHeader + 117
        bs04_ld_sts = cntHeader + 118
        bs04_hm_aw = cntHeader + 119
        bs05_ld_no = cntHeader + 120
        bs05_ld_sts = cntHeader + 121
        bs05_hm_aw = cntHeader + 122
        bs06_ld_no = cntHeader + 123
        bs06_ld_sts = cntHeader + 124
        bs06_hm_aw = cntHeader + 125
        bof_dr1_ld_no = cntHeader + 126
        bof_dr1_ld_sts = cntHeader + 127
        bof_dr1_hm_aw = cntHeader + 128
        bof_dr2_ld_no = cntHeader + 129
        bof_dr2_ld_sts = cntHeader + 130
        bof_dr2_hm_aw = cntHeader + 131
        bof_dr3_ld_no = cntHeader + 132
        bof_dr3_ld_sts = cntHeader + 133
        bof_dr3_hm_aw = cntHeader + 134
        bof_dr4_ld_no = cntHeader + 135
        bof_dr4_ld_sts = cntHeader + 136
        bof_dr4_hm_aw = cntHeader + 137
        bof_dr5_ld_no = cntHeader + 138
        bof_dr5_ld_sts = cntHeader + 139
        bof_dr5_hm_aw = cntHeader + 140
        bof_dr6_ld_no = cntHeader + 141
        bof_dr6_ld_sts = cntHeader + 142
        bof_dr6_hm_aw = cntHeader + 143
        pigm_ld_no = cntHeader + 144
        pigm_ld_sts = cntHeader + 145
        pigm_hm_aw = cntHeader + 146
        dump_ld_no = cntHeader + 147
        dump_ld_sts = cntHeader + 148
        dump_hm_aw = cntHeader + 149
        rlds1_ld_no = cntHeader + 150
        rlds1_ld_sts = cntHeader + 151
        rlds1_hm_aw = cntHeader + 152
        rlds2_ld_no = cntHeader + 153
        rlds2_ld_sts = cntHeader + 154
        rlds2_hm_aw = cntHeader + 155
        elc1_ld_no = cntHeader + 156
        elc1_ld_sts = cntHeader + 157
        elc1_hm_aw = cntHeader + 158
        elc2_ld_no = cntHeader + 159
        elc2_ld_sts = cntHeader + 160
        elc2_hm_aw = cntHeader + 161
        wbof1_ld_no = cntHeader + 162
        wbof1_ld_sts = cntHeader + 163
        wbof1_hm_aw = cntHeader + 164
        wbof2_ld_no = cntHeader + 165
        wbof2_ld_sts = cntHeader + 166
        wbof2_hm_aw = cntHeader + 167
        wbof3_ld_no = cntHeader + 168
        wbof3_ld_sts = cntHeader + 169
        wbof3_hm_aw = cntHeader + 170
        eaf_dr1_ld_no = cntHeader + 171
        eaf_dr1_ld_sts = cntHeader + 172
        eaf_dr1_hm_aw = cntHeader + 173
        eaf_dr2_ld_no = cntHeader + 174
        eaf_dr2_ld_sts = cntHeader + 175
        eaf_dr2_hm_aw = cntHeader + 176
        weaf_ld_no = cntHeader + 177
        weaf_ld_sts = cntHeader + 178
        weaf_hm_aw = cntHeader + 179
        rep_bof_ld = cntHeader + 180
        rep_eaf_ld = cntHeader + 181
    End Enum

    'Index of MS10 Structure 
    Public Enum idxMsgMS10
        process_date = 1
        bof1_heatno = cntHeader + 0
        bof1_heat_sts_1 = cntHeader + 1
        bof1_heat_sts_2 = cntHeader + 2
        bof1_evt_time = cntHeader + 3
        lts1_heatno_1 = cntHeader + 4
        lts1_heatno_2 = cntHeader + 5
        lts1_virt_heatno = cntHeader + 6
        lts1_heat_sts = cntHeader + 7
        lts1_evt_time = cntHeader + 8
        bof2_heatno = cntHeader + 9
        bof2_heat_sts_1 = cntHeader + 10
        bof2_heat_sts_2 = cntHeader + 11
        bof2_evt_time = cntHeader + 12
        lts2_heatno_1 = cntHeader + 13
        lts2_heatno_2 = cntHeader + 14
        lts2_virt_heatno = cntHeader + 15
        lts2_heat_sts = cntHeader + 16
        lts2_evt_time = cntHeader + 17
        bof3_heatno = cntHeader + 18
        bof3_heat_sts_1 = cntHeader + 19
        bof3_heat_sts_2 = cntHeader + 20
        bof3_evt_time = cntHeader + 21
        lts3_heatno_1 = cntHeader + 22
        lts3_heatno_2 = cntHeader + 23
        lts3_virt_heatno = cntHeader + 24
        lts3_heat_sts = cntHeader + 25
        lts3_evt_time = cntHeader + 26
        rh1_heatno = cntHeader + 27
        rh1_heat_sts = cntHeader + 28
        rh1_evt_time = cntHeader + 29
        rh2_heatno = cntHeader + 30
        rh2_heat_sts = cntHeader + 31
        rh2_evt_time = cntHeader + 32
        stn1_heatno = cntHeader + 33
        stn1_heat_sts = cntHeader + 34
        stn1_evt_time = cntHeader + 35
        inj1_heatno = cntHeader + 36
        inj1_heat_sts = cntHeader + 37
        inj1_evt_time = cntHeader + 38

    End Enum

    'Index of MS11 Structure 
    Public Enum idxMsgMS11
        process_date = 1
        dyr1_ld_no = cntHeader + 0
        dyr1_ld_sts = cntHeader + 1
        dyr1_s_aw = cntHeader + 2
        dyr2_ld_no = cntHeader + 3
        dyr2_ld_sts = cntHeader + 4
        dyr2_s_aw = cntHeader + 5
        dyr3_ld_no = cntHeader + 6
        dyr3_ld_sts = cntHeader + 7
        dyr3_s_aw = cntHeader + 8
        dyr4_ld_no = cntHeader + 9
        dyr4_ld_sts = cntHeader + 10
        dyr4_s_aw = cntHeader + 11
        dyr5_ld_no = cntHeader + 12
        dyr5_ld_sts = cntHeader + 13
        dyr5_s_aw = cntHeader + 14
        dyr6_ld_no = cntHeader + 15
        dyr6_ld_sts = cntHeader + 16
        dyr6_s_aw = cntHeader + 17
        phr1_ld_no = cntHeader + 18
        phr1_ld_sts = cntHeader + 19
        phr1_s_aw = cntHeader + 20
        phr2_ld_no = cntHeader + 21
        phr2_ld_sts = cntHeader + 22
        phr2_s_aw = cntHeader + 23
        phr3_ld_no = cntHeader + 24
        phr3_ld_sts = cntHeader + 25
        phr3_s_aw = cntHeader + 26
        lts1_ld_no = cntHeader + 27
        lts1_ld_sts = cntHeader + 28
        lts1_s_aw = cntHeader + 29
        lts2_ld_no = cntHeader + 30
        lts2_ld_sts = cntHeader + 31
        lts2_s_aw = cntHeader + 32
        lts3_ld_no = cntHeader + 33
        lts3_ld_sts = cntHeader + 34
        lts3_s_aw = cntHeader + 35
        rh1_ld_no = cntHeader + 36
        rh1_ld_sts = cntHeader + 37
        rh1_s_aw = cntHeader + 38
        rh2_ld_no = cntHeader + 39
        rh2_ld_sts = cntHeader + 40
        rh2_s_aw = cntHeader + 41
        inj1_ld_no = cntHeader + 42
        inj1_ld_sts = cntHeader + 43
        inj1_s_aw = cntHeader + 44
        stn1_ld_no = cntHeader + 45
        stn1_ld_sts = cntHeader + 46
        stn1_s_aw = cntHeader + 47
        lf1_ld_no = cntHeader + 48
        lf1_ld_sts = cntHeader + 49
        lf1_s_aw = cntHeader + 50
        scc1_pp_ld_no = cntHeader + 51
        scc1_pp_ld_sts = cntHeader + 52
        scc1_pp_s_aw = cntHeader + 53
        scc1_cp_ld_no = cntHeader + 54
        scc1_cp_ld_sts = cntHeader + 55
        scc1_cp_s_aw = cntHeader + 56
        scc2_pp_ld_no = cntHeader + 57
        scc2_pp_ld_sts = cntHeader + 58
        scc2_pp_s_aw = cntHeader + 59
        scc2_cp_ld_no = cntHeader + 60
        scc2_cp_ld_sts = cntHeader + 61
        scc2_cp_s_aw = cntHeader + 62
        scc3_pp_ld_no = cntHeader + 63
        scc3_pp_ld_sts = cntHeader + 64
        scc3_pp_s_aw = cntHeader + 65
        scc3_cp_ld_no = cntHeader + 66
        scc3_cp_ld_sts = cntHeader + 67
        scc3_cp_s_aw = cntHeader + 68

    End Enum

    'Index of MS12 Structure 
    Public Enum idxMsgMS12
        process_date = 1
        input_code = cntHeader + 0
        event_id = cntHeader + 1
        rail_no = cntHeader + 2
        data_time = cntHeader + 3
        ld_no = cntHeader + 4
        old_ld_no = cntHeader + 5
        charge_stime = cntHeader + 6
        charge_etime = cntHeader + 7
        tare_weight = cntHeader + 8
        request_weigth = cntHeader + 9
        residual_weigth = cntHeader + 10
        charge_weigth = cntHeader + 11
        recharge_flag = cntHeader + 12
        ld_sts = cntHeader + 13

    End Enum

    'Index of MS13 Structure 
    Public Enum idxMsgMS13
        process_date = 1
        input_code = cntHeader + 0
        data_time = cntHeader + 1
        tl11_ld_no = cntHeader + 2
        tl12_ld_no = cntHeader + 3
        tl13_ld_no = cntHeader + 4
        tl14_ld_no = cntHeader + 5
        tl15_ld_no = cntHeader + 6
        tl16_ld_no = cntHeader + 7
        tl11_ld_sts = cntHeader + 8
        tl12_ld_sts = cntHeader + 9
        tl13_ld_sts = cntHeader + 10
        tl14_ld_sts = cntHeader + 11
        tl15_ld_sts = cntHeader + 12
        tl16_ld_sts = cntHeader + 13
        tl11_weight = cntHeader + 14
        tl12_weight = cntHeader + 15
        tl13_weight = cntHeader + 16
        tl14_weight = cntHeader + 17
        tl15_weight = cntHeader + 18
        tl16_weight = cntHeader + 19

    End Enum

    'Index of MS14 Structure 
    Public Enum idxMsgMS14
        process_date = 1
        bof_heat_no = cntHeader + 0
        ld_no = cntHeader + 1
        hm_chg_weight = cntHeader + 2
        bof_chg_stime = cntHeader + 3
        sys_process_date = cntHeader + 4

    End Enum

    'Index of MS15 Structure 
    Public Enum idxMsgMS15
        process_date = 1
        hm_ld_no = cntHeader + 0
        hm_aw = cntHeader + 1
        cd_time = cntHeader + 2
        sys_process_date = cntHeader + 3

    End Enum

    'Index of PP31 Structure 
    Public Enum idxMsgPP31
        process_date = 1
        time1 = cntHeader + 0
        output = cntHeader + 1
        consum = cntHeader + 2
        bof_w = cntHeader + 3
        ef_w = cntHeader + 4
        no1_bof_w = cntHeader + 5
        no2_bof_w = cntHeader + 6
        treat_date = cntHeader + 7

    End Enum

    'Index of PP32 Structure 
    Public Enum idxMsgPP32
        process_date = 1
        time1 = cntHeader + 0
        output = cntHeader + 1
        consum = cntHeader + 2
        bof_w = cntHeader + 3
        treat_date = cntHeader + 4

    End Enum

    'Index of PP33 Structure 
    Public Enum idxMsgPP33
        process_date = 1
        facility = cntHeader + 0
        flow = cntHeader + 1
        std_flow = cntHeader + 2
        raw_wt = cntHeader + 3
        out_no_1 = cntHeader + 4
        out_no_2 = cntHeader + 5
        out_no_3 = cntHeader + 6
        roll_no_1 = cntHeader + 7
        roll_no_2 = cntHeader + 8
        roll_no_3 = cntHeader + 9
        roll_no_4 = cntHeader + 10
        roll_no_5 = cntHeader + 11
        roll_no_6 = cntHeader + 12

    End Enum

    'Index of PP99 Structure 
    Public Enum idxMsgPP99
        process_date = 1
        bfid = cntHeader + 0
        bv = cntHeader + 1
    End Enum

    'Index of Schedule Structure 
    Public Enum idxMsgSchedule
        id = 0
        eq_id = 1
        mtn_content = 2
        stime = 3
        mtn_time = 4
    End Enum

    'Index of Eaf_data Structure 
    Public Enum idxMsgEaf_data
        eaf_1_heatno = 1
        eaf_1_sts = 2
        eaf_2_heatno = 3
        eaf_2_sts = 4
        lf1_1_heatno = 5
        lf1_1_sts = 6
        lf1_2_heatno = 7
        lf1_2_sts = 8
        vd_heatno = 9
        vd_sts = 10
        bcc_heatno = 11
        bcc_sts = 12
        btc_heatno = 13
        btc_sts = 14
        eaf_desc = 15
        login_user = 16
        login_ip = 17

    End Enum

    'Index of Sld_data Structure 
    Public Enum idxMsgSld_data
        turn01_ld_no = 1
        turn01_ld_sts = 2
        turn02_ld_no = 3
        turn02_ld_sts = 4
        turn03_ld_no = 5
        turn03_ld_sts = 6
        mak_brick01_ld_no = 7
        mak_brick01_ld_sts = 8
        mak_brick02_ld_no = 9
        mak_brick02_ld_sts = 10
        blk_brick01_ld_no = 11
        blk_brick01_ld_sts = 12
        blk_brick02_ld_no = 13
        blk_brick02_ld_sts = 14
        blk_brick03_ld_no = 15
        blk_brick03_ld_sts = 16
        blk_brick04_ld_no = 17
        blk_brick04_ld_sts = 18
        blk_brick05_ld_no = 19
        blk_brick05_ld_sts = 20
        blk_brick06_ld_no = 21
        blk_brick06_ld_sts = 22
        login_uid = 23
        login_ip = 24

    End Enum
    'Index of SP74 Structure 
    Public Enum idxMsgSP74
        process_date = 1
        heatno = cntHeader + 0
        orderno = cntHeader + 1
        steel_grade = cntHeader + 2
        heat_seq_no = cntHeader + 3
        ld_stl_w = cntHeader + 4
        ld_stl_temp = cntHeader + 5
        tund_stl_w = cntHeader + 6
        tund_temp = cntHeader + 7
        sr_opa_1 = cntHeader + 8
        sr_opa_2 = cntHeader + 9
        osci_feq_1 = cntHeader + 10
        osci_feq_2 = cntHeader + 11
        cast_speed_1 = cntHeader + 12
        cast_speed_2 = cntHeader + 13
        mold_lvl_act_1 = cntHeader + 14
        mold_lvl_act_2 = cntHeader + 15
        osci_ctl_no_1 = cntHeader + 16
        osci_ctl_no_2 = cntHeader + 17
        ld_otime = cntHeader + 18
        est_ld_ctime = cntHeader + 19
    End Enum

    'Index of TQ75 Structure 
    Public Enum idxMsgTQ75
        process_date = 1
        heatno = cntHeader + 0
        orderno = cntHeader + 1
        geno = cntHeader + 2
        curr_fac = cntHeader + 3
        heat_sts = cntHeader + 4
        data_date = cntHeader + 5
        f_heatno = cntHeader + 6
        n_heatno = cntHeader + 7
        act_proc_code = cntHeader + 8
        stl_grade = cntHeader + 9
        grade_grp = cntHeader + 10
        ld_no = cntHeader + 11
        act_ref_code = cntHeader + 12
        tund_stl_w = cntHeader + 13
        ld_opn_len_1 = cntHeader + 14
        ld_opn_len_2 = cntHeader + 15

    End Enum

    'Index of bf_limit Structure 
    Public Enum idxMsgbf_limit
        bv = 0
        pc = 1
        cr = 2
        fuel = 3
        o2 = 4
        dpv = 5
        tft = 6
        solu = 7
        tt = 8
        slagv = 9
        hmt = 10
        hmsi = 11
        hms = 12
        slagb2 = 13
        hb1 = 14
        hb2 = 15
        hb3 = 16
        hb4 = 17
        hw1 = 18
        hw2 = 19
        hw3 = 20
        hw4 = 21
        hw5 = 22
        ub1 = 23
        ub2 = 24
        hlsub = 25
        oilw = 26
        tp = 27
        bm = 28
        bt = 29
        ccr = 30
        uco = 31
        scr = 32

    End Enum

    'Index of sp_limit Structure 
    Public Enum idxMsgsp_limit
        b2dev = 0
        ISO63 = 1
        RDI = 2
        ms = 3

    End Enum

    'Index of cok_limit Structure 
    Public Enum idxMsgcok_limit
        ms = 0

    End Enum

End Module
