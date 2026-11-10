// SPDX-License-Identifier: MIT

namespace Fahrenheit.Mods.Seymour;

public unsafe partial class SeymourModule : FhModule {
    // Aeons/Healer/Stoic/Comrade/Warrior
    int h_MsLimitTypeDamageCheck(int attacker_id, Chr* attacker, int target_id, Chr* target, int param_5, int param_6, int param_7) {
        int iVar1;
        int iVar2;
        Chr* chr;
        int iVar4;
        int iVar5;
        bool BVar6;
        bool BVar7;

        iVar4 = param_5;
        iVar5 = 0;
        BVar6 = FhXCall.MsGetRamChrMonster.fnptr!(attacker_id);
        BVar7 = FhXCall.MsGetRamChrMonster.fnptr!(target_id);
        if ((target->ram.limit_mode_selected == 0x13) && (-1 < param_5) && (BVar6 == true) && (BVar7 == false)) { // Aeons Only
            FhXCall.MsLimitUp.fnptr!(target_id, target, (param_6 * 0x12) / target->ram.max_hp + 1);
        }
        if ((param_5 < 0) && (BVar6 == false) && BVar7 == false && (attacker_id != target_id)) {
            iVar5 = 1;
            FhXCall.FUN_003b10c0.fnptr!(attacker_id, 3, 0);
            if (attacker->ram.limit_mode_selected == 3) { // Healer
                iVar1 = target->ram.max_hp;
                iVar2 = iVar1 - target->ram.hp;
                iVar4 = -param_5;
                if (-iVar2 != param_5 && iVar2 <= -param_5) {
                    iVar4 = iVar2;
                }
                FhXCall.MsLimitUp.fnptr!(attacker_id, attacker, (iVar4 << 4) / iVar1 + 1);
            }
        }
        else if (param_5 > 0 && BVar6 == true && BVar7 == false) {
            FhXCall.FUN_003b10c0.fnptr!(target_id, 2, 0);
            if (target->ram.limit_mode_selected == 2) { // Stoic
                FhXCall.MsLimitUp.fnptr!(target_id, target, (param_5 * 0x1e) / target->ram.max_hp + 1);
            }
            iVar5 = 0;
            param_5 = 1;
            do {
                chr = FhXCall.MsGetChr.fnptr!(iVar5);
                if ((chr->in_battle != 0) && (iVar5 != target_id)) {
                    param_5 = param_5 + 1;
                    FhXCall.FUN_003b10c0.fnptr!(iVar5, 1, 0);
                    if (chr->ram.limit_mode_selected == 1) { // Comrade
                        FhXCall.MsLimitUp.fnptr!(iVar5, chr, (iVar4 * 0x14) / target->ram.max_hp + 1);
                    }
                }
                iVar5 = iVar5 + 1;
            } while (iVar5 < 8);
            return param_5;
        }
        else if ((BVar6 == false) && (BVar7 == true) && param_5 > 0 && (param_7 != 0)) {
            iVar5 = 1;
            FhXCall.FUN_003b10c0.fnptr!(attacker_id, 0, 0);
            if (attacker->ram.limit_mode_selected == 0) { // Warrior
                iVar4 = (param_5 * 10) / *(int*)((byte*)attacker + 0x6f4) + 1;
                if (0x10 < iVar4) {
                    iVar4 = 0x10;
                }
                FhXCall.MsLimitUp.fnptr!(attacker_id, attacker, iVar4);
            }
            if (attacker->ram.limit_mode_selected == 0x13) { // Aeons Only
                FhXCall.MsLimitUp.fnptr!(attacker_id, attacker, ((param_5 << 4) / *(int*)((byte*)attacker + 0x6f4)) / 10 + 1);
                return 1;
            }
        }
        return iVar5;
    }

    // Avenger/Slayer/Hero
    int h_MsLimitTypeDeathCheck(int attacker_id, Chr* attacker, int target_id, Chr* target) {
        int iVar1;
        Chr* chr;
        int iVar3;
        bool BVar4;
        bool BVar5;

        iVar3 = 0;
        BVar4 = FhXCall.MsGetRamChrMonster.fnptr!(attacker_id);
        BVar5 = FhXCall.MsGetRamChrMonster.fnptr!(target_id);
        if (BVar4 == true && BVar5 == false) {
            iVar1 = 0;
            do {
                chr = FhXCall.MsGetChr.fnptr!(iVar1);
                if ((chr->in_battle != 0) && (iVar1 != target_id)) {
                    iVar3 = iVar3 + 1;
                    FhXCall.FUN_003b10c0.fnptr!(iVar1, 7, 0);
                    if (chr->ram.limit_mode_selected == 7) { // Avenger
                        FhXCall.MsLimitUp.fnptr!(iVar1, chr, 30);
                    }
                }
                iVar1 = iVar1 + 1;
            } while (iVar1 < 8);
            return iVar3;
        }
        else if ((BVar4 == false) && (BVar5 == true)) {
            FhXCall.FUN_003b10c0.fnptr!(attacker_id, 8, 0);
            if (attacker->ram.limit_mode_selected == 8) { // Slayer
                FhXCall.MsLimitUp.fnptr!(attacker_id, attacker, 20);
            }
            iVar1 = target->ram.max_hp;
            if ((*(int*)((byte*)attacker + 0x6f4) * 20 < iVar1) || (9999 < iVar1)) {
                FhXCall.FUN_003b10c0.fnptr!(attacker_id, 9, 0);
            }
            if ((attacker->ram.limit_mode_selected == 9) && ((uint)(*(int*)((byte*)attacker + 0x6f4) * 3) < (uint)target->ram.max_hp)) { // Hero
                FhXCall.MsLimitUp.fnptr!(attacker_id, attacker, 20);
            }
        }
        return 0;
    }

    int h_FUN_003b10c0(int chr_id, uint limit_mode, int param_3) {
        Chr* chr = FhXCall.MsGetChr.fnptr!(chr_id);

        if (Globals.Battle.btl->battle_type == 0 && chr_id < 8 && limit_mode < 0x11 && chr->stat_death == 0 && chr->stat_stone == 0 || param_3 != 0) {
            int mask = 1 << ((byte)limit_mode & 0x1F);
            if ((*(int*)((byte*)chr + 0x6F0) & mask) == 0) {
                PlySave* ply_save = &Globals.save_data->ply_saves[chr_id];
                if (ply_save->limit_mode_counters[(int)limit_mode] != 0xFFFF) {
                    *(int*)((byte*)chr + 0x6F0) |= mask;
                    if (ply_save->limit_mode_counters[(int)limit_mode] != 0) {
                        ply_save->limit_mode_counters[(int)limit_mode] -= 1;
                    }
                    if (ply_save->limit_mode_counters[(int)limit_mode] == 0 && !ply_save->obtained_limit_modes.HasFlag((OverdriveModeFlags)limit_mode)) {
                        *(byte*)((nint)Globals.Battle.btl + 0x175B) = 1;
                        return 1;
                    }
                }
            }
        }
        return 0;
    }

    // Ally/Daredevil/Loner/Sufferer
    int h_MsLimitTypeTurnCheck(int chr_id, Chr* chr) {
        int chr_id_00;
        int iVar2;
        Chr* pCVar3;
        int chr_id_01;
        int local_8;

        chr_id_00 = chr_id;
        if (7 < chr_id) {
            return 0;
        }
        local_8 = 1;
        FhXCall.FUN_003b10c0.fnptr!(chr_id, 0xd, 0);
        if (chr->ram.limit_mode_selected == 0xd) { // Ally
            FhXCall.MsLimitUp.fnptr!(chr_id, chr, 3);
        }
        iVar2 = FhXCall.MsCalcWeakLevel.fnptr!(chr->ram.hp, chr->ram.max_hp);
        if (0 < iVar2) {
            local_8 = 2;
            FhXCall.FUN_003b10c0.fnptr!(chr_id, 0xf, 0);
            if (chr->ram.limit_mode_selected == 0xf) { // Daredevil
                FhXCall.MsLimitUp.fnptr!(chr_id, chr, 5);
            }
        }
        iVar2 = 0;
        chr_id = 0;
        chr_id_01 = 0;
        do {
            pCVar3 = FhXCall.MsGetChr.fnptr!(chr_id_01);
            if ((chr_id_01 != chr_id_00) &&
                (pCVar3->in_battle != 0) &&
                (pCVar3->stat_action != 0) &&
                (pCVar3->stat_death == 0) &&
                (pCVar3->stat_stone == 0)) {
                chr_id = chr_id + 1;
                iVar2 = chr_id;
            }
            chr_id_01 = chr_id_01 + 1;
        } while (chr_id_01 < 0x12);
        if (iVar2 == 0) {
            local_8 = local_8 + 1;
            FhXCall.FUN_003b10c0.fnptr!(chr_id_00, 0x10, 0);
            if (chr->ram.limit_mode_selected == 0x10) { // Loner
                FhXCall.MsLimitUp.fnptr!(chr_id_00, chr, 0x10);
            }
        }
        if (((chr->ram.status_suffer & (StatusPermanentFlags.POISON | StatusPermanentFlags.ZOMBIE)) == StatusPermanentFlags.NONE) &&
            ((chr->ram.status_suffer & StatusPermanentFlags.CONFUSION) == StatusPermanentFlags.NONE) &&
             (chr->ram.status_suffer_turns_left.sleep == 0) &&
             (chr->ram.status_suffer_turns_left.silence == 0) &&
             (chr->ram.status_suffer_turns_left.darkness == 0) &&
             (chr->ram.status_suffer_turns_left.slow == 0) &&
            ((chr->ram.status_suffer_extra & StatusExtraFlags.DOOM) == StatusExtraFlags.NONE)) {
            return local_8;
        }
        FhXCall.FUN_003b10c0.fnptr!(chr_id_00, 0xe, 0);
        if (chr->ram.limit_mode_selected == 0xe) { // Sufferer
            FhXCall.MsLimitUp.fnptr!(chr_id_00, chr, 0x10);
        }
        return local_8 + 1;
    }

    // Victor
    int h_MsLimitTypeWinCheck() {
        Chr* chr;
        int iVar1;
        int chr_id;

        iVar1 = 0;
        chr_id = 0;
        do {
            chr = FhXCall.MsGetChr.fnptr!(chr_id);
            if (chr->in_battle != 0) {
                iVar1 = iVar1 + 1;
                FhXCall.FUN_003b10c0.fnptr!(chr_id, 0xb, 0);
                if (chr->ram.limit_mode_selected == 0xb) { // Victor
                    FhXCall.MsLimitUp.fnptr!(chr_id, chr, 0x14);
                }
            }
            chr_id = chr_id + 1;
        } while (chr_id < 8);
        return iVar1;
    }
}