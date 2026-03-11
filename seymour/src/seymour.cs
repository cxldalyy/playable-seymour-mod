// SPDX-License-Identifier: MIT

using Fahrenheit.Atel;
using Fahrenheit.FFX.Battle;
using System;
using System.Runtime.InteropServices;
using System.Text;
using TerraFX.Interop.Windows;

namespace Fahrenheit.Mods.Seymour;

/* TO-DO:
 *   MsExchangeChr = Saves characters after a character is switched
 *   MsExchangePlayer = Saves characters after something?
 *   MsExchangeParty = Saves party formation after something?
 *   MsExchangeSummon = Saves characters after an aeon is summoned or dismissed
 *   FUN_008d85f0 = Equipment abilities in shop windows (0xFC794D in FFX.exe)
 *   Adjust menu textures in field menu/battle rewards to accomodate an extra character
 *   Fix softlocking Spare Change/Bribe/Threaten/Provoke/Grenade abilities for Seymour
*/

[FhLoad(FhGameId.FFX)]
public unsafe class SeymourModule : FhModule {
    /* [fkelava 27/6/25 00:30]
    * A module's constructor must be parameterless. Use it to initialize local fields and objects.
    * Fahrenheit initialization is performed in `init` instead. Read that method's XML documentation comment for more details.
    */

    //Global Function Calls
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate SaveData* MsGetSaveEventAddress();
    private readonly MsGetSaveEventAddress _AtelGetEventSaveRamAdrs = FhUtil.get_fptr<MsGetSaveEventAddress>(FhCall.__addr_MsGetSaveEventAddress);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void MsGetSavePartyMember(uint* ref_frontline_0, uint* ref_frontline_1, uint* ref_frontline_2);
    private readonly MsGetSavePartyMember _MsGetSavePartyMember = FhUtil.get_fptr<MsGetSavePartyMember>(FhCall.__addr_MsGetSavePartyMember);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate bool MsGetSavePlyJoin(uint chr_id);
    private readonly MsGetSavePlyJoin _MsGetSavePlyJoin = FhUtil.get_fptr<MsGetSavePlyJoin>(FhCall.__addr_MsGetSavePlyJoin);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void MsSetSavePlyJoin(uint _chr_id, int enable);
    private readonly MsSetSavePlyJoin _MsSetSavePlyJoin = FhUtil.get_fptr<MsSetSavePlyJoin>(0x386A70);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void FUN_00786a10(uint param_1, uint param_2, uint param_3);
    private readonly FUN_00786a10 _FUN_00786a10 = FhUtil.get_fptr<FUN_00786a10>(0x386A10);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int AtelPopStackInteger(AtelBasicWorker* work, AtelStack* stack);
    private readonly AtelPopStackInteger _AtelPopStackInteger = FhUtil.get_fptr<AtelPopStackInteger>(0x46DE90);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate PlySave* MsGetSavePlayerPtr(uint chr_id);
    private readonly MsGetSavePlayerPtr _MsGetSavePlayerPtr = FhUtil.get_fptr<MsGetSavePlayerPtr>(0x3853F0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int FUN_00798be0(BtlRewardData* get_data);
    private readonly FUN_00798be0 _FUN_00798be0 = FhUtil.get_fptr<FUN_00798be0>(0x398BE0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate byte MsGetSavePlyJoined(byte idx);
    private readonly MsGetSavePlyJoined _MsGetSavePlyJoined = FhUtil.get_fptr<MsGetSavePlyJoined>(0x385460);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate uint Brnd(int param_1);
    private readonly Brnd _Brnd = FhUtil.get_fptr<Brnd>(0x398900);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int MsCheckRange(int n, int min, int max);
    private readonly MsCheckRange _MsCheckRange = FhUtil.get_fptr<MsCheckRange>(0x39A0D0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate uint FUN_00798aa0(uint param_1);
    private readonly FUN_00798aa0 _FUN_00798aa0 = FhUtil.get_fptr<FUN_00798aa0>(0x398AA0);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate byte MsWeaponNameNum(Equipment* gear);
    private readonly MsWeaponNameNum _MsWeaponNameNum = FhUtil.get_fptr<MsWeaponNameNum>(0x3A0D10);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate Equipment* MsGetSaveWeapon(uint gear_inv_idx, byte** ref_name);
    private readonly MsGetSaveWeapon _MsGetSaveWeapon = FhUtil.get_fptr<MsGetSaveWeapon>(FhCall.__addr_MsGetSaveWeapon);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void TkMn2GetExcelData(int req_elem_idx/*, ExcelDataFile* excel_data_ptr*/);
    private readonly TkMn2GetExcelData _TkMn2GetExcelData = FhUtil.get_fptr<TkMn2GetExcelData>(0x4C1AD0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void FUN_008d9140(uint param_1);
    private readonly FUN_008d9140 _FUN_008d9140 = FhUtil.get_fptr<FUN_008d9140>(0x4D9140);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate byte FUN_008a9c20(uint param_1);
    private readonly FUN_008a9c20 _FUN_008a9c20 = FhUtil.get_fptr<FUN_008a9c20>(0x4A9C20);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate byte FUN_008a97d0(uint param_1);
    private readonly FUN_008a97d0 _FUN_008a97d0 = FhUtil.get_fptr<FUN_008a97d0>(0x4A97D0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate float graphicUiRemapY2(float y);
    private readonly graphicUiRemapY2 _graphicUiRemapY2 = FhUtil.get_fptr<graphicUiRemapY2>(0x2449D0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate float graphicUiRemapX2(float x);
    private readonly graphicUiRemapX2 _graphicUiRemapX2 = FhUtil.get_fptr<graphicUiRemapX2>(0x244990);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void TODrawMenuPlateXYWHType(float x, float y, float w, float h, int type);
    private readonly TODrawMenuPlateXYWHType _TODrawMenuPlateXYWHType = FhUtil.get_fptr<TODrawMenuPlateXYWHType>(0x4F5F70);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void FUN_008f8bb0(int param_1, float param_2, float param_3, float param_4, float param_5);
    private readonly FUN_008f8bb0 _FUN_008f8bb0 = FhUtil.get_fptr<FUN_008f8bb0>(0x4F8BB0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void FUN_008bee40(uint param_1);
    private readonly FUN_008bee40 _FUN_008bee40 = FhUtil.get_fptr<FUN_008bee40>(0x4BEE40);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void ToGetBtlEasyFontWidth(byte* text, float* ref_width, int param_3, float scale);
    private readonly ToGetBtlEasyFontWidth _ToGetBtlEasyFontWidth = FhUtil.get_fptr<ToGetBtlEasyFontWidth>(FhCall.__addr_ToGetBtlEasyFontWidth);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void ToMakeBtlEasyFont(byte* param_1, float param_2, float param_3, float param_4, float param_5);
    private readonly ToMakeBtlEasyFont _ToMakeBtlEasyFont = FhUtil.get_fptr<ToMakeBtlEasyFont>(FhCall.__addr_ToMakeBtlEasyFont);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void FUN_008d8a70(float param_1, float param_2, int param_3);
    private readonly FUN_008d8a70 _FUN_008d8a70 = FhUtil.get_fptr<FUN_008d8a70>(0x4D8A70);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DrawCrossMenuIconWeaponName2(ushort* param_1, float param_2, float param_3, float param_4);
    private readonly DrawCrossMenuIconWeaponName2 _DrawCrossMenuIconWeaponName2 = FhUtil.get_fptr<DrawCrossMenuIconWeaponName2>(0x4E6970);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate uint MsGetRamChrMonster(uint mon_id);
    private readonly MsGetRamChrMonster _MsGetRamChrMonster = FhUtil.get_fptr<MsGetRamChrMonster>(FhCall.__addr_MsGetRamChrMonster);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate uint MsLimitUp(int param_1, Chr* character, uint init_limit_add);
    private readonly MsLimitUp _MsLimitUp = FhUtil.get_fptr<MsLimitUp>(FhCall.__addr_MsLimitUp);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int MsCalcWeakLevel(int current_hp, int max_hp);
    private readonly MsCalcWeakLevel _MsCalcWeakLevel = FhUtil.get_fptr<MsCalcWeakLevel>(FhCall.__addr_MsCalcWeakLevel);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate byte TkMenuGetCurrentPlayer();
    private readonly TkMenuGetCurrentPlayer _TkMenuGetCurrentPlayer = FhUtil.get_fptr<TkMenuGetCurrentPlayer>(FhCall.__addr_TkMenuGetCurrentPlayer);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate Chr* MsGetChr(uint chr_id);
    private readonly MsGetChr _MsGetChr = FhUtil.get_fptr<MsGetChr>(FhCall.__addr_MsGetChr);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate Command* MsGetRomPlyCommand(uint com_id, int* param_2);
    private readonly MsGetRomPlyCommand _MsGetRomPlyCommand = FhUtil.get_fptr<MsGetRomPlyCommand>(FhCall.__addr_MsGetRomPlyCommand);


    //Pointers
    private uint* p_DAT_0186ab60 => FhUtil.ptr_at<uint>(0x0146ab60);

    private uint* p_DAT_0186aadc_curShopIdx => FhUtil.ptr_at<uint>(0x0146aadc);

    private uint* p_DAT_0186ab68_arms_shop => FhUtil.ptr_at<uint>(0x0146ab68);

    private byte* toBwNum => FhUtil.ptr_at<byte>(0x01fcc092);

    //Summon Static Handling
    private static ushort[] _filteredSummonList = new ushort[8];
    private static ushort* _filteredSummonPtr;
    private static GCHandle _filteredSummonHandle;


    //Method Handle Functions
    //Celestial
    private ushort* _NkSeymourLegend = FhUtil.ptr_at<ushort>(0x886D80);

    //New Game Init
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void MsSetSaveStartGame();
    private readonly FhMethodHandle<MsSetSaveStartGame> _MsSetSaveStartGame;

    //Party
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int AtelPushMember(AtelBasicWorker* work, int* storage, AtelStack* stack);
    private readonly FhMethodHandle<AtelPushMember> _AtelPushMember;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int FUN_0086dd40(AtelBasicWorker* work, int* storage, AtelStack* stack);
    private readonly FhMethodHandle<FUN_0086dd40> _FUN_0086dd40;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int CT_RetInt_0171_restoreCharHp(AtelBasicWorker* work, int* storage, AtelStack* stack);
    private readonly FhMethodHandle<CT_RetInt_0171_restoreCharHp> _CT_RetInt_0171_restoreCharHp;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int CT_RetInt_0172_restoreCharMp(AtelBasicWorker* work, int* storage, AtelStack* stack);
    private readonly FhMethodHandle<CT_RetInt_0172_restoreCharMp> _CT_RetInt_0172_restoreCharMp;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate uint MsExchangeChr(uint switcher_id, uint switchee_id, int param_3, int param_4);
    private readonly FhMethodHandle<MsExchangeChr> _MsExchangeChr;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void MsExchangeParty();
    private readonly FhMethodHandle<MsExchangeParty> _MsExchangeParty;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void MsExchangePlayer();
    private readonly FhMethodHandle<MsExchangePlayer> _MsExchangePlayer;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void MsExchangeSummon();
    private readonly FhMethodHandle<MsExchangeSummon> _MsExchangeSummon;

    //Equipment
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate byte* MsWeaponName(int name_id, int owner, int hiragana, ushort* ref_model_id);
    private readonly FhMethodHandle <MsWeaponName> _MsWeaponName;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int MsGetItemInternal_00798C20(int param_1, int param_2, int param_3);
    private readonly FhMethodHandle<MsGetItemInternal_00798C20> _MsGetItemInternal_00798C20;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void MsChangeWeaponInvisible(uint param_1, byte param_2);
    private readonly FhMethodHandle<MsChangeWeaponInvisible> _MsChangeWeaponInvisible;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void FUN_008d85f0(int param_1, int param_2);
    private readonly FhMethodHandle<FUN_008d85f0> _FUN_008d85f0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int FUN_00635c20(uint param_1);
    private readonly FhMethodHandle<FUN_00635c20> _FUN_00635c20;

    //Overdrive Modes
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int MsLimitTypeDamageCheck(uint param_1, int param_2, uint param_3, int param_4, int param_5, int param_6, int param_7);
    private readonly FhMethodHandle<MsLimitTypeDamageCheck> _MsLimitTypeDamageCheck;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int MsLimitTypeDeathCheck(int param_1, int param_2, uint param_3, int param_4);
    private readonly FhMethodHandle<MsLimitTypeDeathCheck> _MsLimitTypeDeathCheck;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int FUN_007b10d0(uint chr_id, uint limit_mode, int param_3);
    private readonly FhMethodHandle<FUN_007b10d0> _FUN_007b10d0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int MsLimitTypeTurnCheck(uint param_1, int param_2);
    private readonly FhMethodHandle<MsLimitTypeTurnCheck> _MsLimitTypeTurnCheck;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int MsLimitTypeWinCheck();
    private readonly FhMethodHandle<MsLimitTypeWinCheck> _MsLimitTypeWinCheck;

    //Summon
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int MsParseCommand(byte* param_1);
    private readonly FhMethodHandle<MsParseCommand> _MsParseCommand;

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate void TOBtlCtrlHelpWin(int param_1);
    private readonly FhMethodHandle<TOBtlCtrlHelpWin> _TOBtlCtrlHelpWin;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate ushort* TOGetSaveWindow(uint chr_id, BtlWindowType window_type, int* summonlistlength);
    private readonly FhMethodHandle<TOGetSaveWindow> _TOGetSaveWindow;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int TkMenuSummonEnableMask();
    private readonly FhMethodHandle<TkMenuSummonEnableMask> _TkMenuSummonEnableMask;


    public SeymourModule() {
        _MsSetSaveStartGame = new(this, "FFX.exe", 0x386BC0, h_MsSetSaveStartGame);
        _AtelPushMember = new(this, "FFX.exe", 0x46E2A0, h_AtelPushMember);
        _FUN_0086dd40 = new(this, "FFX.exe", 0x46DD40, h_FUN_0086dd40);
        _CT_RetInt_0171_restoreCharHp = new(this, "FFX.exe", 0x45C4F0, h_CT_RetInt_0171_restoreCharHp);
        _CT_RetInt_0172_restoreCharMp = new(this, "FFX.exe", 0x45C6B0, h_CT_RetInt_0172_restoreCharMp);
        //_MsExchangeChr = new(this, "FFX.exe", 0x3ADAF0, h_MsExchangeChr);
        _MsExchangeParty = new(this, "FFX.exe", 0x3ADE20, h_MsExchangeParty);
        _MsExchangePlayer = new(this, "FFX.exe", 0x3ADF20, h_MsExchangePlayer);
        _MsExchangeSummon = new(this, "FFX.exe", 0x3ADFA0, h_MsExchangeSummon);
        _MsWeaponName = new(this, "FFX.exe", 0x3A0C70, h_MsWeaponName);
        _MsGetItemInternal_00798C20 = new(this, "FFX.exe", 0x398C20, h_MsGetItemInternal_00798C20);
        _MsChangeWeaponInvisible = new(this, "FFX.exe", 0x3AD5F0, h_MsChangeWeaponInvisible);
        _FUN_008d85f0 = new(this, "FFX.exe", 0x4D85F0, h_FUN_008d85f0);
        _FUN_00635c20 = new(this, "FFX.exe", 0x235C20, h_FUN_00635c20);
        _MsLimitTypeDamageCheck = new(this, "FFX.exe", 0x3B0D60, h_MsLimitTypeDamageCheck);
        _MsLimitTypeDeathCheck = new(this, "FFX.exe", 0x3B0F90, h_MsLimitTypeDeathCheck);
        _FUN_007b10d0 = new(this, "FFX.exe", 0x3B10D0, h_FUN_007b10d0);
        _MsLimitTypeTurnCheck = new(this, "FFX.exe", 0x3B13D0, h_MsLimitTypeTurnCheck);
        _MsLimitTypeWinCheck = new(this, "FFX.exe", 0x3B1550, h_MsLimitTypeWinCheck);
        _MsParseCommand = new(this, "FFX.exe", 0x3AE380, h_MsParseCommand);
        _TOBtlCtrlHelpWin = new(this, "FFX.exe", 0x491250, h_TOBtlCtrlHelpWin);
        _TOGetSaveWindow = new(this, "FFX.exe", 0x49B510, h_TOGetSaveWindow);
        _TkMenuSummonEnableMask = new(this, "FFX.exe", 0x4AB190, h_TkMenuSummonEnableMask);
    }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        _NkSeymourLegend[0] = 0x8019; // Break Damage Limit
        _NkSeymourLegend[1] = 0x800F; // Triple Overdrive
        _NkSeymourLegend[2] = 0x8006; // Magic Booster
        _NkSeymourLegend[3] = 0x800D; // One MP Cost
        _MsSetSaveStartGame.hook(); // Initialise Seymour's settings/status for a new game
        _AtelPushMember.hook(); // Store Party Setup
        _FUN_0086dd40.hook(); // Restore Party Setup
        _CT_RetInt_0171_restoreCharHp.hook();
        _CT_RetInt_0172_restoreCharMp.hook();
        //_MsExchangeChr.hook();
        //_MsExchangeParty.hook();
        //_MsExchangePlayer.hook();
        //_MsExchangeSummon.hook();
        _MsWeaponName.hook();
        _MsGetItemInternal_00798C20.hook(); // Include character 7's equipment in battle drops
        _MsChangeWeaponInvisible.hook(); // Hide/Unhide character equipment in menus
        //_FUN_008d85f0.hook(); // Displays equipment abilities in shop windows
        _FUN_00635c20.hook(); // Unhide Seymour's Armor Model (4067h)
        _MsLimitTypeDamageCheck.hook(); // Warrior/Stoic/Healer/Comrade/Aeon Overdrive Modes
        _MsLimitTypeDeathCheck.hook(); // Avenger/Slayer/Hero Overdrive Modes
        _FUN_007b10d0.hook(); // Overdrive Mode Learning
        _MsLimitTypeTurnCheck.hook(); // Ally/Sufferer/Daredevil/Loner Overdrive Modes
        _MsLimitTypeWinCheck.hook(); // Victor Overdrive Mode
        _MsParseCommand.hook(); // Summon execution
        _TOBtlCtrlHelpWin.hook(); // "Summon the aeon Anima." help text for custom summon command in battle
        _TOGetSaveWindow.hook(); // List of summonable Aeons in battle menu
        _TkMenuSummonEnableMask.hook(); // List of Aeons in the field/party Overdrive list menu
        return true;
    }

    public override void load_local_state(FileStream? local_state_file, FhLocalStateInfo local_state_info) {}
    public override void save_local_state(FileStream local_state_file) {}

    //New Game Init
    private void h_MsSetSaveStartGame() {
        _MsSetSaveStartGame.orig_fptr();

        Globals.save_data->ability_map_limit.has_extra_24 = true;
        Globals.save_data->ability_map_limit.has_extra_25 = true;

        for (int i = 0; i < 200; i++) {
            Equipment* gear = &Globals.save_data->equipment[i];
            if (gear->exists && gear->owner == 7) {
                gear->flags = 2;
                if (gear->type == 1) {
                    gear->slot_count = 1;
                    gear->abilities[0] = 0xFF;
                    gear->name_id = _MsWeaponNameNum(gear);
                }
                else {
                    gear->slot_count = 2;
                    gear->abilities[1] = 0x8000;
                    gear->name_id = _MsWeaponNameNum(gear);
                }
                _MsWeaponName.hook_fptr(gear->name_id, gear->owner, 0, &gear->model_id);
            }
        }

        PlySave* seymour = _MsGetSavePlayerPtr(7);
        seymour->base_mp = 319;
        seymour->mp = 319;
        seymour->max_mp = 319;
        seymour->slv_spent = 40;
        seymour->abi_map.has_weapon_change = true;
        seymour->abi_map.has_armor_change = true;
        seymour->limit_mode_ctr_warrior = 150;
        seymour->limit_mode_ctr_comrade = 240;
        seymour->limit_mode_ctr_healer = 100;
        seymour->limit_mode_ctr_tactician = 75;
        seymour->limit_mode_ctr_victim = 80;
        seymour->limit_mode_ctr_dancer = 200;
        seymour->limit_mode_ctr_avenger = 160;
        seymour->limit_mode_ctr_slayer = 115;
        seymour->limit_mode_ctr_hero = 70;
        seymour->limit_mode_ctr_rook = 110;
        seymour->limit_mode_ctr_victor = 180;
        seymour->limit_mode_ctr_coward = 700;
        seymour->limit_mode_ctr_ally = 320;
        seymour->limit_mode_ctr_sufferer = 65;
        seymour->limit_mode_ctr_daredevil = 150;
        seymour->limit_mode_ctr_loner = 30;
        seymour->obtained_limit_modes = (OverdriveModeFlags)(uint)OverdriveModeFlags.STOIC;

        Command* requiem = _MsGetRomPlyCommand(0x30E3, (int*)0x0);
        requiem->flags_menu = 0;
        requiem->sub_menu_cat = 4;
        requiem->is_piercing = true;
        requiem->flags_damage = 4;
        requiem->dmg_formula = 15;

        Command* extra24 = _MsGetRomPlyCommand(0x3130, (int*)0x0);
        extra24->name_offset = 12487;
        extra24->desc_offset = 12496;
        extra24->icon = 19;
        extra24->opens_sub_menu = true;
        extra24->sub_menu_cat2 = 5;
        extra24->sub_menu_cat = 4;
        extra24->user_id = 7;
        extra24->flags_target = 0;
        extra24->display_move_name = false;
        extra24->is_in_trigger_menu = true;
        extra24->show_user_casting_effects = true;
        extra24->limit_cost = 100;

        Command* extra25 = _MsGetRomPlyCommand(0x3131, (int*)0x0);
        extra25->is_top_level_in_menu = true;
        extra25->opens_sub_menu = true;
        extra25->sub_menu_cat2 = 4;
        extra25->sub_menu_cat = 4;
        extra25->user_id = 7;
        extra25->flags_target = 0;
        extra25->display_move_name = false;
        extra25->is_in_trigger_menu = true;
        extra25->show_user_casting_effects = true;
        extra25->limit_cost = 100;
    }

    //Party
    //D81101 saves party members join status (AtelPushMember), D81201 restores party members, + their join status (FUN_0086dd40) -
    //no need to manually readd party members in ATEL.
    int h_AtelPushMember(AtelBasicWorker* work, int* storage, AtelStack* stack) {
        byte bVar1;
        SaveData *pSVar2;
        BOOL BVar3;
        byte *puVar4;
        uint *puVar5;
        int iVar6;
        uint uVar7;
        uint chr_id;
        uint* local_14 = stackalloc uint[4];

        //local_14[3] = __security_cookie ^ (uint)&stack0xfffffffc;
        pSVar2 = _AtelGetEventSaveRamAdrs();
        bVar1 = pSVar2->atel_is_push_member;
        _MsGetSavePartyMember(local_14, local_14 + 1, local_14 + 2);
        puVar4 = &pSVar2->atel_push_frontline[0];
        puVar5 = local_14;
        iVar6 = 3;
        do {
            if (*puVar5 == 0xff) {
                *puVar4 = 0xff;
            }
            else {
                *puVar4 = (byte)*puVar5;
            }
            puVar5 = puVar5 + 1;
            puVar4 = puVar4 + 1;
            iVar6 = iVar6 + -1;
        } while (iVar6 != 0);
        chr_id = 0;
        *(int*)&pSVar2->atel_push_party = 0;
        uVar7 = 1;
        do {
            BVar3 = _MsGetSavePlyJoin(chr_id);
            if (BVar3 == 1) {
                *(uint*)&pSVar2->atel_push_party = *(uint*)&pSVar2->atel_push_party | uVar7;
            }
            uVar7 = uVar7 << 1 | (uint)((int)uVar7 < 0 ? 1 : 0);
            chr_id = chr_id + 1;
        } while ((int)chr_id < 8);
        pSVar2->atel_is_push_member = 1;
        return bVar1;
    }

    int h_FUN_0086dd40(AtelBasicWorker* work, int* storage, AtelStack* stack) {
        byte bVar1;
        SaveData *pSVar2;
        uint uVar3;
        uint uVar4;
        uint uVar5;

        pSVar2 = _AtelGetEventSaveRamAdrs();
        bVar1 = pSVar2->atel_is_push_member;
        if (bVar1 != 0) {
            uVar4 = 0;
            uVar5 = 1;
            do {
                _MsSetSavePlyJoin(uVar4, (int)(((*(uint*)&pSVar2->atel_push_party & uVar5) != 0) ? 1u : 0u));
                uVar5 = uVar5 << 1 | (((int)uVar5 < 0) ? 1u : 0u);
                uVar4 = uVar4 + 1;
            } while ((int)uVar4 < 8);
            uVar4 = pSVar2->atel_push_frontline[0];
            if (pSVar2->atel_push_frontline[0] == 0xff) {
                uVar4 = 0xff;
            }
            uVar5 = pSVar2->atel_push_frontline[1];
            if (pSVar2->atel_push_frontline[1] == 0xff) {
                uVar5 = 0xff;
            }
            uVar3 = pSVar2->atel_push_frontline[2];
            if (pSVar2->atel_push_frontline[2] == 0xff) {
                uVar3 = 0xff;
            }
            _FUN_00786a10(uVar4, uVar5, uVar3);
        }
        pSVar2->atel_is_push_member = 0;
        return bVar1;
    }

    int h_CT_RetInt_0171_restoreCharHp(AtelBasicWorker* work, int* storage, AtelStack* stack) {
        uint chr_id;
        PlySave *ply_save;

        chr_id = (uint)_AtelPopStackInteger(work, stack);
        ply_save = _MsGetSavePlayerPtr(chr_id);
        ply_save->__0x3D = 0;
        if (ply_save->max_hp < ply_save->hp) {
            return (int)ply_save->hp;
        }
        ply_save->hp = ply_save->max_hp;
        if (chr_id == 3) {
            PlySave* seymour = _MsGetSavePlayerPtr(7);
            seymour->__0x3D = 0;
            seymour->hp = seymour->max_hp;
        }
        return (int)ply_save->max_hp;
    }

    int h_CT_RetInt_0172_restoreCharMp(AtelBasicWorker* work, int* storage, AtelStack* stack) {
        uint chr_id;
        PlySave *ply_save;

        chr_id = (uint)_AtelPopStackInteger(work, stack);
        ply_save = _MsGetSavePlayerPtr(chr_id);
        if (ply_save->max_mp < ply_save->mp) {
            return (int)ply_save->mp;
        }
        ply_save->mp = ply_save->max_mp;
        if (chr_id == 3) {
            PlySave* seymour = _MsGetSavePlayerPtr(7);
            seymour->mp = seymour->max_mp;
        }
        return (int)ply_save->max_mp;
    }

    /*private uint h_MsExchangeChr(uint switcher_id, uint switchee_id, int param_3, int param_4) {
        
    }*/

    //TODO: Add functionality for Seymour
    void h_MsExchangeParty() {
        byte bVar1;
        byte bVar2;
        byte bVar3;
        byte uVar4;
        byte uVar5;
        byte uVar6;
        byte uVar7;
        int iVar8;
        Chr* pMVar9;
        uint chr_id;

        bVar1 = Globals.Battle.btl->__0x1FC5[0];
        Globals.Battle.btl->__0x1FC5[0] = Globals.Battle.btl->__0x1FCC[0];
        bVar2 = Globals.Battle.btl->__0x1FC5[1];
        Globals.Battle.btl->__0x1FC5[1] = Globals.Battle.btl->__0x1FCC[1];
        bVar3 = Globals.Battle.btl->__0x1FC5[2];
        Globals.Battle.btl->__0x1FC5[2] = Globals.Battle.btl->__0x1FCC[2];
        uVar4 = Globals.Battle.btl->__0x1FC5[3];
        Globals.Battle.btl->__0x1FC5[3] = Globals.Battle.btl->__0x1FCC[3];
        uVar5 = Globals.Battle.btl->__0x1FC5[4];
        Globals.Battle.btl->__0x1FC5[4] = Globals.Battle.btl->__0x1FCC[4];
        uVar6 = Globals.Battle.btl->__0x1FC5[5];
        Globals.Battle.btl->__0x1FC5[5] = Globals.Battle.btl->__0x1FCC[5];
        uVar7 = Globals.Battle.btl->__0x1FC5[6];
        Globals.Battle.btl->__0x1FC5[6] = Globals.Battle.btl->__0x1FCC[6];
        iVar8 = 0;
        Globals.Battle.btl->__0x1FCC[0] = bVar1;
        Globals.Battle.btl->__0x1FCC[1] = bVar2;
        Globals.Battle.btl->__0x1FCC[2] = bVar3;
        Globals.Battle.btl->__0x1FCC[3] = uVar4;
        Globals.Battle.btl->__0x1FCC[4] = uVar5;
        Globals.Battle.btl->__0x1FCC[5] = uVar6;
        Globals.Battle.btl->__0x1FCC[6] = uVar7;
        do
        {
            bVar1 = Globals.Battle.btl->__0x1FD3[iVar8 + 0x11];
            Globals.Battle.btl->__0x1FD3[iVar8 + 0x11] = Globals.Battle.btl->__0x1FD3[iVar8];
            Globals.Battle.btl->__0x1FD3[iVar8] = bVar1;
            iVar8 = iVar8 + 1;
        } while (iVar8 < 0x11);
        chr_id = 0;
        do
        {
            pMVar9 = _MsGetChr(chr_id);
            //bVar1 = pMVar9->__0xDC9;
            chr_id = chr_id + 1;
            //pMVar9->__0xDC9 = pMVar9->in_battle;
            pMVar9->in_battle = bVar1;
        } while ((int)chr_id < 0x12);
        return;
    }

    void h_MsExchangePlayer() {

    }

    void h_MsExchangeSummon() {

    }


    //Equipment
    private static string[] _seymour_weapon_names = [
        "Dimittis", //Celestial
        "Scepter", //Brotherhood
        "Subduing Scepter", //Capture
        "Arcane Scepter", //4x Elemental Strikes
        "Heaven Fall", //Break Damage Limit
        "Transcendence", //Triple Overdrive + Triple AP + Overdrive > AP
        "Retribution", //Triple Overdrive + Overdrive > AP
        "Deliverance", //Double Overdrive + Double AP
        "Ferrier of Souls", //Triple Overdrive
        "Veil Piercer", //Double Overdrive
        "Benediction", //Triple AP
        "Rite of the Guado", //Double AP
        "Sublimator", //Overdrive > AP
        "Fettered Malice", //SOS Overdrive
        "Scepter", //Dummy?
        "Astral Scepter", //One MP Cost
        "Chaos Scepter", //4x Status Strikes
        "Scepter", //Dummy?
        "Scepter", //Dummy?
        "Master Scepter", //4x Strength Bonuses
        "Wizard's Scepter", //4x Magic Bonuses
        "Mana Scepter", //3x Magic +X%s + Magic Booster
        "Magistral Scepter", //Half MP Cost
        "Resplendence", //Gillionaire
        "Tri-Scepter", //At least 3x Elemental Strikes
        "Malefic Scepter", //At least 3x Status Strikes
        "Nemesis Scepter", //Magic Counter + either Counterattack or Evade & Counter
        "Karmic Scepter", //Either Counterattack or Evade & Counter
        "P-Scepter", //Distill Power
        "M-Scepter", //Distill Mana
        "S-Scepter", //Distill Speed
        "A-Scepter", //Distill Ability
        "Prism Scepter", //Magic Counter
        "Mirage Scepter", //Magic Booster
        "Thaumaturge", //Alchemy
        "Sonic Scepter", //First Strike
        "Quick Gambit", //Initiative
        "Grim Embrace", //Deathstrike
        "Halting Grace", //Slowstrike
        "Earth Breaker", //Stonestrike
        "Serpent's Fang", //Poisonstrike
        "Eternal Slumber", //Sleepstrike
        "Inhibition", //Silencestrike
        "Nightfall", //Darkstrike
        "Monk's Scepter", //At least 3x Strength +X%s
        "Priest's Scepter", //At least 3x Magic +X%s
        "Dual Scepter", //At least 2x Element Strikes
        "Ominous Scepter", //At least 2x Status Touch's
        "Atrophy Scepter", //Deathtouch
        "Languid Scepter", //Slowtouch
        "Break Scepter", //Stonetouch
        "Miasma Scepter", //Poisontouch
        "Hypno Scepter", //Sleeptouch
        "Tranquil Scepter", //Silencetouch
        "Twilight Scepter", //Darktouch
        "Scout Scepter", //Sensor
        "Flame Scepter", //Firestrike
        "Frost Scepter", //Icestrike
        "Blitz Scepter", //Lightningstrike
        "Flood Scepter", //Waterstrike
        "Futile Scepter", //4x Empty Slots
        "Force Scepter", //At least x1 Strength +X% and x1 Magic +X%
        "Vain Scepter", //At least 2x Empty Slots
        "Sorcery Scepter", //Magic +10% or Magic +20%
        "Decimator Scepter", //Strength +10% or Strength +20%
        "Rune Scepter", //Magic +5%
        "Enchanted Scepter", //Magic +3%
        "Buster Scepter", //Strength +5%
        "Ruin Scepter", //Strength +3%
        "Spiked Scepter", //Piercing
        "Scepter", //Else
        "Scepter", //Dummy?
        "Scepter", //Dummy?
        "Scepter", //Dummy?
        "Resolute", //Break HP Limit + Break MP Limit
        "Arcane Circlet", //Break HP Limit
        "Mythical Circlet", //Break MP Limit
        "Crystal Circlet", //4x Element Eaters
        "Aegis Circlet", //4x Element Proofs
        "Unwavering", //Auto-Reflect + Auto-Regen + Auto-Protect + Auto-Shell
        "Renatus", //Auto-Phoenix + Auto-Med + Auto-Potion
        "Restorative Circlet", //Auto-Potion + Auto-Med
        "Omnis", //4x Status Proofs
        "Diamond Circlet", //4x Defense +X%s
        "Ruby Circlet", //4x Magic Def +X%s
        "Empowered Circlet", //4x HP +X%s
        "Magical Circlet", //4x MP +X%s
        "Collector Circlet", //Master Thief
        "Treasure Circlet", //Pickpocket
        "Circlet of Hope", //HP Stroll + MP Stroll
        "Assault Circlet", //4x Auto's
        "Phantom Circlet", //3x Element Eaters
        "Recovery Circlet", //HP Stroll
        "Spiritual Circlet", //MP Stroll
        "Phoenix Circlet", //Auto-Phoenix
        "Curative Circlet", //Auto-Med
        "Rainbow Circlet", //4x SOS Nuls
        "Shining Circlet", //4x SOS'
        "Faerie Circlet", //At least 3x Status Proofs
        "Peaceful Circlet", //No Encounters
        "Shaman Circlet", //Auto-Potion
        "Barrier Circlet", //At least 3x Element Proofs
        "Star Circlet", //At least 3x SOS'
        "Marching Circlet", //At least 2x Auto's
        "Moon Circlet", //At least 2x SOS'
        "Regen Circlet", //Auto-Regen or SOS Regen
        "Haste Circlet", //Auto-Haste or SOS Haste
        "Reflect Circlet", //Auto-Reflect or SOS Reflect
        "Shell Circlet", //Auto-Shell or SOS Shell
        "Protect Circlet", //Auto-Protect or SOS Protect
        "Circlet", //Alchemy
        "Platinum Circlet", //At least 3x Defense +X%s
        "Sapphire Circlet", //At least 3x Magic Def +X%s
        "Power Circlet", //At least 3x HP +X%s
        "Wizard Circlet", //At least 3x MP +X%s
        "Elemental Circlet", //At least 2x Elemental Proofs or Eaters
        "Savior Circlet", //At least 2x Status Proofs
        "Crimson Circlet", //Fire Eater
        "Snow Circlet", //Ice Eater
        "Ochre Circlet", //Lightning Eater
        "Cerulean Circlet", //Water Eater
        "Medical Circlet", //Curseproof or Curse Ward
        "Lucid Circlet", //Confuseproof or Confuse Ward
        "Serene Circlet", //Berserkproof or Berserk Ward
        "Light Circlet", //Slowproof or Slow Ward
        "Soul Circlet", //Deathproof or Death Ward
        "Blessed Circlet", //Zombieproof or Zombie Ward
        "Soft Circlet", //Stoneproof or Stone Ward
        "Serum Circlet", //Poisonproof or Poison Ward
        "Alert Circlet", //Sleepproof or Sleep Ward
        "Echo Circlet", //Silenceproof or Silence Ward
        "Bright Circlet", //Darkproof or Dark Ward
        "Red Circlet", //Fireproof or Fire Ward
        "White Circlet", //Iceproof or Ice Ward
        "Yellow Circlet", //Lightningproof or Lightning Ward
        "Blue Circlet", //Waterproof or Water Ward
        "NulTide Circlet", //SOS NulTide
        "NulBlaze Circlet", //SOS NulBlaze
        "NulShock Circlet", //SOS NulShock
        "NulFrost Circlet", //SOS NulFrost
        "Adept's Circlet", //4x HP +X%s or MP +X%s
        "Tetra Circlet", //4x Empty Slots
        "Mythril Circlet", //At least 1 Def +X% and 1 Magic Def +X%
        "Gold Circlet", //At least 2x Def +X%s
        "Emerald Circlet", //At least 2x Magic Def +X%s
        "Vita Circlet", //At least 2x HP +X%s
        "Mage's Circlet", //At least 2x MP +X%s
        "Silver Circlet", //Def +10% or Def +20%
        "Onyx Circlet", //Magic Def +10% or Magic Def +20%
        "Sorcery Circlet", //MP +20% or MP +30%
        "Tough Circlet", //HP +20% or MP + 20%
        "Glorious Circlet", //3x Empty Slots
        "Metal Circlet", //Def +3% or Def + 5%
        "Pearl Circlet", //Magic Def +3% or Magic Def + 5%
        "Magic Circlet", //MP +5% or MP + 10%
        "Seeker's Circlet", //HP +5% or HP + 10%
        "Guardian Circlet", //2x Empty Slots
        "Circlet", //Else
        "Absolution", //Ribbon
        "Circlet", //Dummy?
        "Circlet", //Dummy?
        "Circlet", //Dummy?
        "Circlet", //Dummy?
        "Circlet", //Dummy?
        "Circlet", //Dummy?
        "Circlet", //Dummy?
        "Circlet", //Dummy?
        "Circlet", //Dummy?
        "Circlet", //Dummy?
        "Circlet", //Dummy?
        "-", //Dummy?
    ];

    private static nint[] _prepared_seymour_weapon_names = new nint[171];

    static SeymourModule() {
        for (int i = 0; i < _prepared_seymour_weapon_names.Length; i++) {
            ReadOnlySpan<byte> weapon_name_utf8 = Encoding.UTF8.GetBytes(_seymour_weapon_names[i]);
            int weapon_name_len = FhEncoding.compute_encode_buffer_size(weapon_name_utf8);
            void* name_ptr = NativeMemory.AllocZeroed((nuint)weapon_name_len + 1);
            _ = FhEncoding.encode(weapon_name_utf8, new(name_ptr, weapon_name_len));
            _prepared_seymour_weapon_names[i] = (nint)name_ptr;
        }
        _filteredSummonHandle = GCHandle.Alloc(_filteredSummonList, GCHandleType.Pinned);
        _filteredSummonPtr = (ushort*)_filteredSummonHandle.AddrOfPinnedObject();
    }

    byte* h_MsWeaponName(int name_id, int owner, int simplified, ushort* ref_model_id) {
        int gearid;

        if (owner != 7)
            return _MsWeaponName.orig_fptr(name_id, owner, simplified, ref_model_id);
        gearid = name_id & 0xFFF;
        if (ref_model_id is not null) {
            if (gearid >= 74) {
                *ref_model_id = 0x4067;
            }
            else {
                *ref_model_id = 0x4066;
            }
        }
        if (gearid > _prepared_seymour_weapon_names.Length - 1)
            return _MsWeaponName.orig_fptr(name_id, owner, simplified, ref_model_id);

        return (byte*)_prepared_seymour_weapon_names[gearid];
    }

    int h_MsGetItemInternal_00798C20(int param_1, int param_2, int param_3) {
        byte bVar1;
        ushort uVar3;
        int iVar4;
        Equipment *gear;
        uint uVar5;
        uint uVar6;
        int iVar7;
        ushort *puVar8;
        int iVar9;
        int iVar10;
        ushort *puVar11;
        int local_14;
        ushort *local_c;
        uint gearOwner;

        iVar4 = _FUN_00798be0((BtlRewardData*)param_3);
        if (iVar4 < 0) {
            return -1;
        }
        iVar4 = iVar4 * 0x16;
        gear = (Equipment*)(param_3 + 0xfe + iVar4);
        *(short*)(iVar4 + 0x100 + param_3) = 1;
        iVar7 = 0;
        iVar9 = 0;
        do {
            bVar1 = _MsGetSavePlyJoined((byte)iVar7);
            if (bVar1 != 0) {
                iVar9 = iVar9 + 1;
            }
            iVar7 = iVar7 + 1;
        } while (iVar7 < 8);
        if (param_1 < 8) {
            iVar9 = iVar9 + 3;
        }
        else {
            param_1 = 0;
        }
        uVar5 = _Brnd(0xc);
        iVar10 = 0;
        iVar7 = 0;
        do {
            if (_MsGetSavePlyJoined((byte)iVar7) != 0) {
                iVar10 = iVar10 + 1;
                if ((int)uVar5 % iVar9 < iVar10) {
                    bVar1 = (byte)iVar7;
                    goto LAB_00798cb8;
                }
            }
            iVar7 = iVar7 + 1;
        } while (iVar7 < 8);
        bVar1 = (byte)param_1;
    LAB_00798cb8:
        *(byte*)(iVar4 + 0x102 + param_3) = bVar1;
        uVar5 = _Brnd(0xc);
        *(byte*)(iVar4 + 0x103 + param_3) = (byte)((byte)uVar5 & 1);
        *(byte*)(iVar4 + 0x104 + param_3) = 0xff;
        *(byte*)(iVar4 + 0x106 + param_3) = *(byte*)(param_2 + 0x2e);
        *(byte*)(iVar4 + 0x107 + param_3) = *(byte*)(param_2 + 0x30);
        *(byte*)(iVar4 + 0x108 + param_3) = *(byte*)(param_2 + 0x2f);
        uVar5 = _Brnd(0xc);
        uVar6 = _Brnd(0xc);
        iVar7 = (int)(*(byte*)(param_2 + 0x2d) + ((uVar5 & 7) - 4));
        iVar7 = _MsCheckRange((int)(iVar7 + (iVar7 >> 0x1f & 3U)) >> 2, 1, 4);
        *(byte*)(iVar4 + 0x109 + param_3) = (byte)iVar7;
        iVar9 = (int)(*(byte*)(param_2 + 0x31) + ((uVar6 & 7) - 4));
        gearOwner = bVar1;
        if (gearOwner == 7) {
            gearOwner = 3; /*Checks if the gear rng rolled belongs to Seymour
                             and assigns the gear's auto-abilities to another character's field
                             (here, Seymour gets Kimahri's auto-abilities)*/
        }
        puVar11 = (ushort*)((byte*)param_2 + 0x32 +
                  (*(byte*)(iVar4 + 0x103 + param_3) + gearOwner * 2
                  ) * 0x10);
        local_14 = (int)(iVar9 + (iVar9 >> 0x1f & 7U)) >> 3;
        uVar3 = *puVar11;
        if (((byte)iVar7 == '\0') || (uVar3 == 0)) {
            iVar4 = 0;
        }
        else {
            gear->abilities[0] = uVar3;
            iVar4 = 1;
        }
        if (0 < local_14) {
            local_c = &gear->abilities[0] + iVar4;
            do {
                if ((int)(uint)gear->slot_count <= iVar4) break;
                uVar5 = _Brnd(0xd);
                uVar3 = puVar11[(int)uVar5 % 7 + 1];
                if (uVar3 != 0) {
                    uVar5 = _FUN_00798aa0(uVar3);
                    iVar7 = 0;
                    if (0 < iVar4) {
                        puVar8 = &gear->abilities[0];
                        do {
                            uVar6 = _FUN_00798aa0(*puVar8);
                            if (uVar6 == uVar5) goto LAB_00798e2c;
                            iVar7 = iVar7 + 1;
                            puVar8 = puVar8 + 1;
                        } while (iVar7 < iVar4);
                    }
                    *local_c = uVar3;
                    iVar4 = iVar4 + 1;
                    local_c = local_c + 1;
                }
            LAB_00798e2c:
                local_14 = local_14 + -1;
            } while (0 < local_14);
        }
        if (iVar4 < 4) {
            puVar11 = &gear->abilities[0] + iVar4;
            for (uVar5 = ((4U - (uint)iVar4) >> 1); uVar5 != 0; uVar5 = uVar5 - 1) {
                *(uint*)puVar11 = 0x00FF00FF;
                puVar11 = puVar11 + 2;
            }
            for (uVar5 = uVar5 = (uint)(((4U - iVar4) & 1) != 0 ? 1 : 0); uVar5 != 0; uVar5 = uVar5 - 1) {
                *puVar11 = 0xff;
                puVar11 = puVar11 + 1;
            }
        }
        uVar3 = _MsWeaponNameNum(gear);
        gear->name_id = uVar3;
        _MsWeaponName.hook_fptr((int)(uint)uVar3, (int)(uint)gear->owner, 0, &gear->model_id);
        return 0;
    }

    void h_MsChangeWeaponInvisible(uint param_1, byte param_2) {
        byte gear;
        Equipment *pSVar2;
        int loop;
        uint chr_id;

        chr_id = param_1 & 0xff;
        if (chr_id < 8) {
            loop = 0;
            do {
                if (loop == 0) {
                    gear = Globals.save_data->ply_saves[(int)chr_id].wpn_inv_idx;
                }
                else {
                    gear = Globals.save_data->ply_saves[(int)chr_id].arm_inv_idx;
                }
                if (gear != 0xff) {
                    pSVar2 = _MsGetSaveWeapon(gear, (byte**)0x0);
                    pSVar2->flags = (byte)(pSVar2->flags ^ (param_2 * '\x02' ^ pSVar2->flags) & 2);
                }
                loop = loop + 1;
            } while (loop < 2);
        }
        return;
    }

    void h_FUN_008d85f0(int param_1, int param_2) {
        /*void *pvVar1;
        Equipment *pSVar2;
        uint gear_inv_idx;
        Equipment *pSVar3;
        byte *pbVar4;
        float fVar5;
        float fVar6;
        float fVar7;
        float fVar8;
        float *pfVar9;
        Equipment *pSVar10;
        int uVar11;
        int scale;
        int iVar12;
        byte *local_14;
        byte *local_10;
        float local_c;
        float local_8;

        if (7 < (uint)p_DAT_0186ab60) {
            return;
        }
        pvVar1 = _TkMn2GetExcelData(p_DAT_0186aadc_curShopIdx, p_DAT_0186ab68_arms_shop);
        if (param_2 == 0) {
            pSVar2 = _MsGetSaveWeapon((uint)p_DAT_01597730_OvrModesMenuList[*(short*)(param_1 + 0x48)].
                                           overdrive_id, &local_10);
        }
        else {
            pSVar2 = (Equipment*)
                     _FUN_008d9140(*(ushort*)((int)pvVar1 + (*(short*)(param_1 + 0x48) * 2) + 2));
        }
        if (pSVar2->type == 0) {
            gear_inv_idx = _FUN_008a9c20((uint)p_DAT_0186ab60);
        }
        else {
            gear_inv_idx = _FUN_008a97d0((uint)p_DAT_0186ab60);
        }
        if (gear_inv_idx == 0xff) {
            pSVar3 = (Equipment*)0x0;
        }
        else {
            pSVar3 = _MsGetSaveWeapon(gear_inv_idx, &local_14);
        }
        iVar12 = 2;
        fVar5 = _graphicUiRemapY2((float)60.0);
        fVar6 = _graphicUiRemapX2((float)740.0);
        fVar7 = _graphicUiRemapY2((float)295.0);
        fVar8 = _graphicUiRemapX2((float)1144.0);
        _TODrawMenuPlateXYWHType(fVar8, fVar7, fVar6, fVar5, iVar12);
        fVar5 = _graphicUiRemapY2((float)36.0);
        fVar6 = _graphicUiRemapX2((float)430.0);
        fVar7 = _graphicUiRemapY2((float)310.0);
        fVar8 = _graphicUiRemapX2((float)1299.0);
        _FUN_008f8bb0(0x12, fVar8, fVar7, fVar6, fVar5);
        if (param_2 == 0) {
            if (pSVar3 == (Equipment*)0x0) {
                iVar12 = 9;
                fVar5 = _graphicUiRemapY2((float)64.0);
                fVar6 = _graphicUiRemapX2((float)700.0);
                fVar7 = _graphicUiRemapY2((float)231.0);
                fVar8 = _graphicUiRemapX2((float)1164.0);
                _TODrawMenuPlateXYWHType(fVar8, fVar7, fVar6, fVar5, iVar12);
                pfVar9 = &local_c;
                scale = 0x3f47ae14;
                uVar11 = 0;
                pbVar4 = (byte*)_FUN_008bee40(0x17);
                _ToGetBtlEasyFontWidth(pbVar4, pfVar9, uVar11, scale);
                fVar7 = (float)0.78;
                pSVar3 = (Equipment*)0x0;
                fVar6 = _graphicUiRemapY2((float)243.0);
                fVar5 = _graphicUiRemapX2((float)1514.0);
                fVar5 = (float)(fVar5 - local_c * 0.5);
                local_8 = fVar5;
                goto LAB_008d8976;
            }
        }
        else if (pSVar3 == (Equipment*)0x0) {
            iVar12 = 9;
            fVar5 = _graphicUiRemapY2((float)64.0);
            fVar6 = _graphicUiRemapX2((float)700.0);
            fVar7 = _graphicUiRemapY2((float)231.0);
            fVar8 = _graphicUiRemapX2((float)1164.0);
            _TODrawMenuPlateXYWHType(fVar8, fVar7, fVar6, fVar5, iVar12);
            pfVar9 = &local_8;
            uVar11 = 0x3f47ae14;
            pSVar10 = pSVar3;
            pbVar4 = (byte*)_FUN_008bee40(0x17);
            _ToGetBtlEasyFontWidth(pbVar4, pfVar9, (int)pSVar10, uVar11);
            fVar7 = (float)0.78;
            fVar6 = _graphicUiRemapY2((float)243.0);
            fVar5 = _graphicUiRemapX2((float)1514.0);
            fVar5 = (float)(fVar5 - local_8 * 0.5);
            local_c = fVar5;
        LAB_008d8976:
            pbVar4 = (byte*)_FUN_008bee40(0x17);
            _ToMakeBtlEasyFont(pbVar4, fVar5, fVar6, (float)pSVar3, fVar7);
            goto LAB_008d898c;
        }
        pSVar10 = pSVar3;
        fVar5 = _graphicUiRemapY2((float)363.0);
        fVar6 = _graphicUiRemapX2((float)1144.0);
        _FUN_008d8a70(fVar6, fVar5, (int)pSVar10);
        iVar12 = 9;
        fVar5 = _graphicUiRemapY2((float)64.0);
        fVar6 = _graphicUiRemapX2((float)700.0);
        fVar7 = _graphicUiRemapY2((float)231.0);
        fVar8 = _graphicUiRemapX2((float)1164.0);
        _TODrawMenuPlateXYWHType(fVar8, fVar7, fVar6, fVar5, iVar12);
        fVar7 = (float)0.0;
        fVar5 = _graphicUiRemapY2((float)231.0);
        fVar6 = _graphicUiRemapX2((float)1164.0);
        _DrawCrossMenuIconWeaponName2(&pSVar3->name_id, fVar6, fVar5, fVar7);
    LAB_008d898c:
        iVar12 = 2;
        fVar5 = _graphicUiRemapY2((float)60.0);
        fVar6 = _graphicUiRemapX2((float)740.0);
        fVar7 = _graphicUiRemapY2((float)659.0);
        fVar8 = _graphicUiRemapX2((float)970.0);
        _TODrawMenuPlateXYWHType(fVar8, fVar7, fVar6, fVar5, iVar12);
        fVar5 = _graphicUiRemapY2((float)36.0);
        fVar6 = _graphicUiRemapX2((float)430.0);
        fVar7 = _graphicUiRemapY2((float)671.0);
        fVar8 = _graphicUiRemapX2((float)1125.0);
        _FUN_008f8bb0(0x13, fVar8, fVar7, fVar6, fVar5);
        fVar5 = _graphicUiRemapY2((float)727.0);
        fVar6 = _graphicUiRemapX2((float)970.0);
        _FUN_008d8a70(fVar6, fVar5, (int)pSVar2);
        return;*/
    }

    int h_FUN_00635c20(uint param_1) {
        if (param_1 == 0x4067) {
            param_1 = 0x4068;
        }
        return _FUN_00635c20.orig_fptr(param_1);
    }


    /* Overdrive Modes:
     *  A function (e.g. MsLimitTypeDeathCheck), handles certain Overdrive Modes and their Overdrive Charge increases. These functions pass into FUN_007b10d0 when
     *  a character achieves a learning requirement specific to that Mode (e.g. Warrior = Character damages an enemy, paramaters pass to FUN_007b10d0).
     *  These functions themselves are what handle the modes Overdrive Charge increases.
     *  FUN_007b10d0 is exclusively for LEARNING Overdrive Modes, not handling their Overdrive Charge increases.
    */
    //Aeon/Healer/Stoic/Comrade/Warrior Overdrive Modes
    int h_MsLimitTypeDamageCheck(uint param_1, int param_2, uint param_3, int param_4, int param_5, int param_6, int param_7) {
        uint uVar1;
        uint uVar2;
        Chr *character;
        int iVar3;
        int iVar4;
        int iVar5;

        iVar5 = param_5;
        iVar4 = 0;
        uVar1 = _MsGetRamChrMonster(param_1);
        uVar2 = _MsGetRamChrMonster(param_3);
        if ((((*(byte*)(param_4 + 0x5bb) == '\x13') && (-1 < param_5)) && (uVar1 == 1)) && (uVar2 == 0)) { //Aeon Stuff
            _MsLimitUp((int)param_3, (Chr*)param_4, (uint)(param_6 * 0x12) / *(uint*)(param_4 + 0x594) + 1);
        }
        if (param_5 < 1) {
            if (((param_5 < 0) && (uVar1 == 0)) && ((uVar2 == 0 && (param_1 != param_3)))) {
                iVar4 = 1;
                _FUN_007b10d0.hook_fptr(param_1, 3, 0); //Decrement Character's Healer Mode Counter
                if (*(byte*)(param_2 + 0x5bb) == '\x03') { //If Character's Overdrive Mode is Healer,
                    iVar3 = (int)(*(uint*)(param_4 + 0x594) - *(int*)(param_4 + 0x5d0));
                    iVar5 = -param_5;
                    if (-iVar3 != param_5 && iVar3 <= -param_5) {
                        iVar5 = iVar3;
                    }
                    _MsLimitUp((int)param_1, (Chr*)param_2, (uint)(iVar5 << 4) / *(uint*)(param_4 + 0x594) + 1); //Increase Character's Overdrive Charge by (Healing Amount * 16) / Target's Max HP + 1.
                }
            }
        }
        else if (uVar1 == 1) {
            if (uVar2 == 0) {
                _FUN_007b10d0.hook_fptr(param_3, 2, 0); //Decrement Character's Stoic Mode Counter
                if (*(byte*)(param_4 + 0x5bb) == '\x02') { //If Character's Overdrive Mode is Stoic,
                    _MsLimitUp((int)param_3, (Chr*)param_4, (uint)(param_5 * 0x1e) / *(uint*)(param_4 + 0x594) + 1); //Increase Character's Overdrive Charge by (Damage Taken * 30) / Max HP + 1.
                }
                uVar1 = 0;
                param_5 = 1;
                do {
                    character = _MsGetChr(uVar1);
                    if ((character->in_battle != 0) && (uVar1 != param_3)) {
                        param_5 = param_5 + 1;
                        _FUN_007b10d0.hook_fptr(uVar1, 1, 0); //Decrement Character's Comrade Mode Counter
                        if (character->ram.limit_mode_selected == 1) { //If Character's Overdrive Mode is Comrade,
                            _MsLimitUp((int)uVar1, character, (uint)(iVar5 * 0x14) / *(uint*)(param_4 + 0x594) + 1); //Increase Character's Overdrive Charge by (Allies' Damage Taken * 20) / Target's Max HP + 1.
                        }
                    }
                    uVar1 = uVar1 + 1;
                } while ((int)uVar1 < 8);
                return param_5;
            }
        }
        else if (((uVar1 == 0) && (uVar2 == 1)) && (param_7 != 0)) {
            iVar4 = 1;
            _FUN_007b10d0.hook_fptr(param_1, 0, 0); //Decrement Character's Warrior Mode Counter
            if (*(byte*)(param_2 + 0x5bb) == '\0') { //If Character's Overdrive Mode is Warrior,
                uVar1 = (uint)((param_5 * 10) / *(int*)(param_2 + 0x6f4) + 1); //
                if (0x10 < (int)uVar1) {
                    uVar1 = 0x10;
                }
                _MsLimitUp((int)param_1, (Chr*)param_2, uVar1); //Increase Character's Overdrive Charge by (Damage Dealt * 10) / Estimated Damage + 1. Maximum gain is 16.
            }
            if (*(byte*)(param_2 + 0x5bb) == '\x13') { //Aeon Stuff
                _MsLimitUp((int)param_1, (Chr*)param_2, (uint)(((param_5 << 4) / *(int*)(param_2 + 0x6f4)) / 10 + 1));
                return 1;
            }
        }
        return iVar4;
    }

    //Avenger/Slayer/Hero Overdrive Mode
    int h_MsLimitTypeDeathCheck(int param_1, int param_2, uint param_3, int param_4) {
        int uVar1;
        uint uVar2;
        Chr *character;
        int iVar3;

        iVar3 = 0;
        uVar1 = (int)_MsGetRamChrMonster((uint)param_1);
        uVar2 = _MsGetRamChrMonster(param_3);
        if (uVar1 == 1) {
            if (uVar2 == 0) {
                uVar1 = 0;
                do {
                    character = _MsGetChr((uint)uVar1);
                    if ((character->in_battle != 0) && (uVar1 != param_3)) {
                        iVar3 = iVar3 + 1;
                        _FUN_007b10d0.hook_fptr((uint)uVar1, 7, 0); //Decrement Character's Avenger Mode Counter
                        if (character->ram.limit_mode_selected == 7) { //If Character's Overdrive Mode is Avenger,
                            _MsLimitUp(uVar1, character, 0x1e); //Increase Character's Overdrive Charge by 30.
                        }
                    }
                    uVar1 = uVar1 + 1;
                } while (uVar1 < 8);
                return iVar3;
            }
        }
        else if ((uVar1 == 0) && (uVar2 == 1)) {
            _FUN_007b10d0.hook_fptr((uint)param_1, 8, 0); //Decrement Character's Slayer Mode Counter
            if (*(byte*)(param_2 + 0x5bb) == '\b') { //If Character's Overdrive Mode is ????,
                _MsLimitUp(param_1, (Chr*)param_2, 0x14); //Increase Character's Overdrive Charge by 20.
            }
            if ((*(int*)(param_2 + 0x6f4) * 0x14 < *(int*)(param_4 + 0x594)) ||
               (9999 < *(int*)(param_4 + 0x594))) { //If Character kills an enemy with at least 10,000 HP, or at least 20x more HP than estimated damage,
                _FUN_007b10d0.hook_fptr((uint)param_1, 9, 0); //Decrement Character's Hero Mode Counter
            }
            if ((*(byte*)(param_2 + 0x5bb) == '\t') &&
               ((uint)(*(int*)(param_2 + 0x6f4) * 3) < *(uint*)(param_4 + 0x594))) { //If Character's Overdrive Mode is Hero?, and kills an enemy with at least 3x more HP than estimated damage,
                _MsLimitUp(param_1, (Chr*)param_2, 0x14); //Increase Character's Overdrive Charge by 20.
            }
        }
        return 0;
    }

    int h_FUN_007b10d0(uint chr_id, uint limit_mode, int param_3) {
        Chr* chr = _MsGetChr(chr_id);

        if (Globals.Battle.btl->battle_type == 0 && chr_id < 8 && limit_mode < 0x11 && chr->stat_death == 0 && chr->stat_stone == 0 || param_3 != 0) {
            int mask = 1 << ((byte)limit_mode & 0x1F);
            if ((*(int*)((byte*)chr + 0x6F0) & mask) == 0) {
                PlySave* ply_save = &Globals.save_data->ply_saves[(int)chr_id];
                if (ply_save->limit_mode_counters[(int)limit_mode] != 0xFFFF) { //If mode can be learned,
                    *(int*)((byte*)chr + 0x6F0) |= mask;
                    if (ply_save->limit_mode_counters[(int)limit_mode] != 0) { //If mode is not already Learned,
                        ply_save->limit_mode_counters[(int)limit_mode] -= 1; //Decrement mode counter by 1.
                    }
                    if (ply_save->limit_mode_counters[(int)limit_mode] == 0 && !ply_save->obtained_limit_modes.HasFlag((OverdriveModeFlags)limit_mode)) {
                        *(byte*)((nint)Globals.Battle.btl + 0x175B) = 1; //Trigger "X has learned Overdrive mode Y!" message.
                        return 1; //Indicate mode was learned.
                    }
                }
            }
        }
        return 0; //Indicate mode was not learned.
    }

    //Ally/Daredevil/Loner/Sufferer Overdrive Modes
    int h_MsLimitTypeTurnCheck(uint param_1, int param_2) {
        uint uVar1;
        int iVar2;
        uint uVar3;
        Chr* character;
        uint chr_id;
        int local_8;

        uVar1 = param_1;
        if (7 < param_1) {
            return 0;
        }
        local_8 = 1;
        _FUN_007b10d0.hook_fptr(param_1, 0xd, 0); //Decrement Character's Ally Mode Counter
        if (*(byte*)(param_2 + 0x5bb) == '\r') { //If Character's Overdrive Mode is Ally,
            _MsLimitUp((int)param_1, (Chr*)param_2, 3); //Increase Character's Overdrive Charge by 3.
        }
        iVar2 = _MsCalcWeakLevel(*(int*)(param_2 + 0x5d0), *(int*)(param_2 + 0x594));
        if (0 < iVar2) {
            local_8 = 2;
            _FUN_007b10d0.hook_fptr(param_1, 0xf, 0); //Decrement Character's Daredevil Mode Counter
            if (*(byte*)(param_2 + 0x5bb) == '\x0f') { //If Character's Overdrive Mode is Daredevil,
                _MsLimitUp((int)param_1, (Chr*)param_2, 5); //Increase Character's Overdrive Charge by 5.
            }
        }
        uVar3 = 0;
        param_1 = 0;
        chr_id = 0;
        do {
            character = _MsGetChr(chr_id);
            uVar3 = param_1;
            if ((chr_id != uVar1) && (character->in_battle != 0) && (character->stat_action != 0) && (character->stat_death == 0) && (character->stat_stone == 0)) {
                param_1 = param_1 + 1;
                uVar3 = param_1;
            }
            chr_id = chr_id + 1;
        } while ((int)chr_id < 0x12);
        if (uVar3 == 0) {
            local_8 = local_8 + 1;
            _FUN_007b10d0.hook_fptr(uVar1, 0x10, 0); //Decrement Character's Loner Mode Counter
            if (*(byte*)(param_2 + 0x5bb) == '\x10') { //If Character's Overdrive Mode is Loner,
                _MsLimitUp((int)uVar1, (Chr*)param_2, 0x10); //Increase Character's Overdrive Charge by 16.
            }
        }
        if (((((*(ushort*)(param_2 + 0x606) & 10) == 0) && ((*(ushort*)(param_2 + 0x606) & 0x100) == 0))
            && ((*(byte*)(param_2 + 0x608) == '\0' &&
                (((*(byte*)(param_2 + 0x609) == '\0' && (*(byte*)(param_2 + 0x60a) == '\0')) &&
                 (*(byte*)(param_2 + 0x614) == '\0')))))) &&
           ((*(ushort*)(param_2 + 0x616) & 0x4000) == 0)) {
            return local_8;
        }
        _FUN_007b10d0.hook_fptr(uVar1, 0xe, 0); //Decrement Character's Sufferer Mode Counter
        if (*(byte*)(param_2 + 0x5bb) == '\x0e') { //If Character's Overdrive Mode is Sufferer,
            _MsLimitUp((int)uVar1, (Chr*)param_2, 0x10); //Increase Character's Overdrive Charge by 16.
        }
        return local_8 + 1;
    }

    //Victor Overdrive Mode
    int h_MsLimitTypeWinCheck() {
        Chr *character;
        int iVar1;
        int chr_id;

        iVar1 = 0;
        chr_id = 0;
        do {
            character = _MsGetChr((uint)chr_id);
            if (character->in_battle != 0) {
                iVar1 = iVar1 + 1;
                _FUN_007b10d0.hook_fptr((uint)chr_id, 0xb, 0); //Decrement Character's Victor Mode Counter
                if (character->ram.limit_mode_selected == 0xb) { //If Character's Overdrive Mode is Victor,
                    _MsLimitUp(chr_id, character, 0x14); //Increase Character's Overdrive Charge by 20.
                }
            }
            chr_id = chr_id + 1;
        } while (chr_id < 8);
        return iVar1;
    }


    //Summon
    int h_MsParseCommand(byte* param_1) {
        uint uVar13 = param_1[2];
        int iVar14 = (int)uVar13 * 0x10;
        ushort* com_id = (ushort*)(param_1 + iVar14 + 8);

        if (*com_id == 0x3130) {
            *com_id = 0x3117;
            int result = _MsParseCommand.orig_fptr(param_1);
            *com_id = 0x3130;
            return result;
        }
        return _MsParseCommand.orig_fptr(param_1);
    }

    void h_TOBtlCtrlHelpWin(int param_1) {
        int win_idx = *toBwNum;
        BtlWindow* currentwindow = &Globals.Battle.windows[win_idx];

        if (currentwindow->window_command_id == 0x3130) {
            currentwindow->window_command_id = 0x3117;
            _TOBtlCtrlHelpWin.orig_fptr(param_1);
            currentwindow->window_command_id = 0x3130;
            return;
        }
        _TOBtlCtrlHelpWin.orig_fptr(param_1);
    }

    ushort* h_TOGetSaveWindow(uint chr_id, BtlWindowType window_type, int* summonlistlength) {
        if ((uint)window_type == 5) {
            ushort* originallist = _TOGetSaveWindow.orig_fptr(chr_id, window_type, summonlistlength);
            int originalcount = *summonlistlength;
            int newcount = 0;

            for (int i = 0; i < originalcount; i++) {
                ushort aeonId = originallist[i];
                if (aeonId == 0xFFFF)
                    break;
                if (chr_id == 7) {
                    if (aeonId == 0x000D) {
                        _filteredSummonList[newcount++] = aeonId;
                    }
                }
                else if (chr_id == 1) {
                    if (aeonId != 0x000D || Globals.save_data->has_anima) {
                        _filteredSummonList[newcount++] = aeonId;
                    }
                }
            }
            _filteredSummonList[newcount] = 0xFFFF;
            *summonlistlength = newcount;
            return _filteredSummonPtr;
        }
        return _TOGetSaveWindow.orig_fptr(chr_id, window_type, summonlistlength);
    }

    //Removes Anima from Yuna's Overdrive menu in field *unless* Anima is unlocked
    int h_TkMenuSummonEnableMask() {
        uint cur_chr_id = _TkMenuGetCurrentPlayer();
        uint original = (uint)_TkMenuSummonEnableMask.orig_fptr();

        if (cur_chr_id == 1) {
            if (!Globals.save_data->has_anima) {
                return (int)(original & ~(1u << 0x0D)); 
            }
        }
        return (int)original;
    }

}
