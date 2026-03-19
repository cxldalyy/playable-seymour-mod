// SPDX-License-Identifier: MIT

using Fahrenheit.Atel;
using Fahrenheit.FFX.Battle;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using TerraFX.Interop.Windows;

namespace Fahrenheit.Mods.Template;

[FhLoad(FhGameId.FFX)]
public unsafe class TemplateModule : FhModule {
    /* [fkelava 27/6/25 00:30]
     * A module's constructor must be parameterless. Use it to initialize local fields and objects.
     * Fahrenheit initialization is performed in `init` instead. Read that method's XML documentation comment for more details.
     */
    const string game = "FFX.exe";
    public TemplateModule() { }
    static TemplateModule()
    {
        string text = "-----";
        ReadOnlySpan<byte> textUtf8 = Encoding.UTF8.GetBytes(text);
        textString = (byte*)NativeMemory.AllocZeroed((nuint)textUtf8.Length + 1);
        textUtf8.CopyTo(new Span<byte>(textString, textUtf8.Length));

        for (int i = 0; i < seymour_gear_names.Length; i++)
        {
            ReadOnlySpan<byte> weapon_name_utf8 = Encoding.UTF8.GetBytes(_seymour_gear_names[i]);
            int weapon_name_len = FhEncoding.compute_encode_buffer_size(weapon_name_utf8);
            void* name_ptr = NativeMemory.AllocZeroed((nuint)weapon_name_len + 1);
            _ = FhEncoding.encode(weapon_name_utf8, new(name_ptr, weapon_name_len));
            seymour_gear_names[i] = (nint)name_ptr;
        }

        _filteredSummonHandle = GCHandle.Alloc(_filteredSummonList, GCHandleType.Pinned);
        _filteredSummonPtr = (ushort*)_filteredSummonHandle.AddrOfPinnedObject();
    }

    private static byte* textString;

    private static nint[] seymour_gear_names = new nint[171];

    private static string[] _seymour_gear_names = [
        "Dimittis",            // Celestial
        "Scepter",             // Brotherhood
        "Subduing Scepter",    // Capture
        "Arcane Scepter",      // 4x Elemental Strikes
        "Heaven Fall",         // Break Damage Limit
        "Transcendence",       // Triple Overdrive + Triple AP + Overdrive > AP
        "Retribution",         // Triple Overdrive + Overdrive > AP
        "Deliverance",         // Double Overdrive + Double AP
        "Ferrier of Souls",    // Triple Overdrive
        "Veil Piercer",        // Double Overdrive
        "Benediction",         // Triple AP
        "Rite of the Guado",   // Double AP
        "Sublimator",          // Overdrive > AP
        "Fettered Malice",     // SOS Overdrive
        "Scepter",             // Dummy?
        "Astral Scepter",      // One MP Cost
        "Chaos Scepter",       // 4x Status Strikes
        "Scepter",             // Dummy?
        "Scepter",             // Dummy?
        "Master Scepter",      // 4x Strength Bonuses
        "Wizard's Scepter",    // 4x Magic Bonuses
        "Mana Scepter",        // 3x Magic +X%s + Magic Booster
        "Magistral Scepter",   // Half MP Cost
        "Resplendence",        // Gillionaire
        "Tri-Scepter",         // At least 3x Elemental Strikes
        "Malefic Scepter",     // At least 3x Status Strikes
        "Nemesis Scepter",     // Magic Counter + either Counterattack or Evade & Counter
        "Karmic Scepter",      // Either Counterattack or Evade & Counter
        "P-Scepter",           // Distill Power
        "M-Scepter",           // Distill Mana
        "S-Scepter",           // Distill Speed
        "A-Scepter",           // Distill Ability
        "Prism Scepter",       // Magic Counter
        "Mirage Scepter",      // Magic Booster
        "Thaumaturge",         // Alchemy
        "Sonic Scepter",       // First Strike
        "Quick Gambit",        // Initiative
        "Grim Embrace",        // Deathstrike
        "Halting Grace",       // Slowstrike
        "Earth Breaker",       // Stonestrike
        "Serpent's Fang",      // Poisonstrike
        "Eternal Slumber",     // Sleepstrike
        "Inhibition",          // Silencestrike
        "Nightfall",           // Darkstrike
        "Monk's Scepter",      // At least 3x Strength +X%s
        "Priest's Scepter",    // At least 3x Magic +X%s
        "Dual Scepter",        // At least 2x Element Strikes
        "Ominous Scepter",     // At least 2x Status Touch's
        "Atrophy Scepter",     // Deathtouch
        "Languid Scepter",     // Slowtouch
        "Break Scepter",       // Stonetouch
        "Miasma Scepter",      // Poisontouch
        "Hypno Scepter",       // Sleeptouch
        "Tranquil Scepter",    // Silencetouch
        "Twilight Scepter",    // Darktouch
        "Scout Scepter",       // Sensor
        "Flame Scepter",       // Firestrike
        "Frost Scepter",       // Icestrike
        "Blitz Scepter",       // Lightningstrike
        "Flood Scepter",       // Waterstrike
        "Futile Scepter",      // 4x Empty Slots
        "Force Scepter",       // At least x1 Strength +X% and x1 Magic +X%
        "Vain Scepter",        // At least 2x Empty Slots
        "Sorcery Scepter",     // Magic +10% or Magic +20%
        "Decimator Scepter",   // Strength +10% or Strength +20%
        "Rune Scepter",        // Magic +5%
        "Enchanted Scepter",   // Magic +3%
        "Buster Scepter",      // Strength +5%
        "Ruin Scepter",        // Strength +3%
        "Spiked Scepter",      // Piercing
        "Scepter",             // Else
        "Scepter",             // Dummy?
        "Scepter",             // Dummy?
        "Scepter",             // Dummy?
        "Resolute",            // Break HP Limit + Break MP Limit
        "Arcane Circlet",      // Break HP Limit
        "Mythical Circlet",    // Break MP Limit
        "Crystal Circlet",     // 4x Element Eaters
        "Aegis Circlet",       // 4x Element Proofs
        "Unwavering",          // Auto-Reflect + Auto-Regen + Auto-Protect + Auto-Shell
        "Renatus",             // Auto-Phoenix + Auto-Med + Auto-Potion
        "Restorative Circlet", // Auto-Potion + Auto-Med
        "Omnis",               // 4x Status Proofs
        "Diamond Circlet",     // 4x Defense +X%s
        "Ruby Circlet",        // 4x Magic Def +X%s
        "Empowered Circlet",   // 4x HP +X%s
        "Magical Circlet",     // 4x MP +X%s
        "Collector Circlet",   // Master Thief
        "Treasure Circlet",    // Pickpocket
        "Circlet of Hope",     // HP Stroll + MP Stroll
        "Assault Circlet",     // 4x Auto's
        "Phantom Circlet",     // 3x Element Eaters
        "Recovery Circlet",    // HP Stroll
        "Spiritual Circlet",   // MP Stroll
        "Phoenix Circlet",     // Auto-Phoenix
        "Curative Circlet",    // Auto-Med
        "Rainbow Circlet",     // 4x SOS Nuls
        "Shining Circlet",     // 4x SOS'
        "Faerie Circlet",      // At least 3x Status Proofs
        "Peaceful Circlet",    // No Encounters
        "Shaman Circlet",      // Auto-Potion
        "Barrier Circlet",     // At least 3x Element Proofs
        "Star Circlet",        // At least 3x SOS'
        "Marching Circlet",    // At least 2x Auto's
        "Moon Circlet",        // At least 2x SOS'
        "Regen Circlet",       // Auto-Regen or SOS Regen
        "Haste Circlet",       // Auto-Haste or SOS Haste
        "Reflect Circlet",     // Auto-Reflect or SOS Reflect
        "Shell Circlet",       // Auto-Shell or SOS Shell
        "Protect Circlet",     // Auto-Protect or SOS Protect
        "Circlet",             // Alchemy
        "Platinum Circlet",    // At least 3x Defense +X%s
        "Sapphire Circlet",    // At least 3x Magic Def +X%s
        "Power Circlet",       // At least 3x HP +X%s
        "Wizard Circlet",      // At least 3x MP +X%s
        "Elemental Circlet",   // At least 2x Elemental Proofs or Eaters
        "Savior Circlet",      // At least 2x Status Proofs
        "Crimson Circlet",     // Fire Eater
        "Snow Circlet",        // Ice Eater
        "Ochre Circlet",       // Lightning Eater
        "Cerulean Circlet",    // Water Eater
        "Medical Circlet",     // Curseproof or Curse Ward
        "Lucid Circlet",       // Confuseproof or Confuse Ward
        "Serene Circlet",      // Berserkproof or Berserk Ward
        "Light Circlet",       // Slowproof or Slow Ward
        "Soul Circlet",        // Deathproof or Death Ward
        "Blessed Circlet",     // Zombieproof or Zombie Ward
        "Soft Circlet",        // Stoneproof or Stone Ward
        "Serum Circlet",       // Poisonproof or Poison Ward
        "Alert Circlet",       // Sleepproof or Sleep Ward
        "Echo Circlet",        // Silenceproof or Silence Ward
        "Bright Circlet",      // Darkproof or Dark Ward
        "Red Circlet",         // Fireproof or Fire Ward
        "White Circlet",       // Iceproof or Ice Ward
        "Yellow Circlet",      // Lightningproof or Lightning Ward
        "Blue Circlet",        // Waterproof or Water Ward
        "NulTide Circlet",     // SOS NulTide
        "NulBlaze Circlet",    // SOS NulBlaze
        "NulShock Circlet",    // SOS NulShock
        "NulFrost Circlet",    // SOS NulFrost
        "Adept's Circlet",     // 4x HP +X%s or MP +X%s
        "Tetra Circlet",       // 4x Empty Slots
        "Mythril Circlet",     // At least 1 Def +X% and 1 Magic Def +X%
        "Gold Circlet",        // At least 2x Def +X%s
        "Emerald Circlet",     // At least 2x Magic Def +X%s
        "Vita Circlet",        // At least 2x HP +X%s
        "Mage's Circlet",      // At least 2x MP +X%s
        "Silver Circlet",      // Def +10% or Def +20%
        "Onyx Circlet",        // Magic Def +10% or Magic Def +20%
        "Sorcery Circlet",     // MP +20% or MP +30%
        "Tough Circlet",       // HP +20% or MP + 20%
        "Glorious Circlet",    // 3x Empty Slots
        "Metal Circlet",       // Def +3% or Def + 5%
        "Pearl Circlet",       // Magic Def +3% or Magic Def + 5%
        "Magic Circlet",       // MP +5% or MP + 10%
        "Seeker's Circlet",    // HP +5% or HP + 10%
        "Guardian Circlet",    // 2x Empty Slots
        "Circlet",             // Else
        "Absolution",          // Ribbon
        "Circlet",             // Dummy?
        "Circlet",             // Dummy?
        "Circlet",             // Dummy?
        "Circlet",             // Dummy?
        "Circlet",             // Dummy?
        "Circlet",             // Dummy?
        "Circlet",             // Dummy?
        "Circlet",             // Dummy?
        "Circlet",             // Dummy?
        "Circlet",             // Dummy?
        "Circlet",             // Dummy?
        "-",                   // Dummy?
    ];

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int AtelPopStackInteger(AtelBasicWorker* work, AtelStack* stack);
    public const nint __addr_AtelPopStackInteger = 0x46DE90;
    private AtelPopStackInteger _AtelPopStackInteger;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate PlySave* MsGetSavePlayerPtr(uint chr_id);
    public const nint __addr_MsGetSavePlayerPtr = 0x3853F0;
    private MsGetSavePlayerPtr _MsGetSavePlayerPtr;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* TOGetShapTextureName(int param_1);
    public const nint __addr_TOGetShapTextureName = 0x4AC870;
    private TOGetShapTextureName _TOGetShapTextureName;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOGetImageWH(int param_1, float* w, float* h);
    public const nint __addr_TOGetImageWH = 0x4AC3B0;
    private TOGetImageWH _TOGetImageWH;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void graphicDrawUIElement(Matrix4f* param_1, byte* param_2, int param_3, int param_4, int param_5);
    public const nint __addr_graphicDrawUIElement = 0x23F090;
    private graphicDrawUIElement _graphicDrawUIElement;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate byte AtelGetAlbhedRikku();
    public const nint __addr_AtelGetAlbhedRikku = 0x46A770;
    private AtelGetAlbhedRikku _AtelGetAlbhedRikku;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate SaveData* MsGetSaveEventAddress();
    public const nint __addr_AtelGetEventSaveRamAdrs = 0x385300;
    private MsGetSaveEventAddress _AtelGetEventSaveRamAdrs;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MsGetSavePartyMember(uint* ref_frontline_0, uint* ref_frontline_1, uint* ref_frontline_2);
    public const nint __addr_MsGetSavePartyMember = 0x3853B0;
    private MsGetSavePartyMember _MsGetSavePartyMember;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool MsGetSavePlyJoin(uint chr_id);
    public const nint __addr_MsGetSavePlyJoin = 0x385440;
    private MsGetSavePlyJoin _MsGetSavePlyJoin;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MsSetSavePlyJoin(uint _chr_id, int enable);
    public const nint __addr_MsSetSavePlyJoin = 0x386A70;
    private MsSetSavePlyJoin _MsSetSavePlyJoin;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_00786a10(uint param_1, uint param_2, uint param_3);
    public const nint __addr_FUN_00786a10 = 0x386A10;
    private FUN_00786a10 _FUN_00786a10;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_0088e6c0(int param_1);
    public const nint __addr_FUN_0088e6c0 = 0x48E6C0;
    private FUN_0088e6c0 _FUN_0088e6c0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_0088e6a0(int param_1);
    public const nint __addr_FUN_0088e6a0 = 0x48E6A0;
    private FUN_0088e6a0 _FUN_0088e6a0;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void TOMenuOpenPktBuffTmp();
    public const nint __addr_TOMenuOpenPktBuffTmp = 0x4BEF00;
    private TOMenuOpenPktBuffTmp _TOMenuOpenPktBuffTmp;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float graphicUiRemapX2(float x);
    public const nint __addr_graphicUiRemapX2 = 0x244990;
    private graphicUiRemapX2 _graphicUiRemapX2;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float graphicUiRemapY2(float y);
    public const nint __addr_graphicUiRemapY2 = 0x2449D0;
    private graphicUiRemapY2 _graphicUiRemapY2;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMkpShapeXYWHUV(int param_1, float x, float y, float w, float h, float uv_x1, float uv_y1, float uv_x2, float uv_y2);
    public const nint __addr_TOMkpShapeXYWHUV = 0x503BB0;
    private TOMkpShapeXYWHUV _TOMkpShapeXYWHUV;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008e7d30(float param_1, float param_2, float param_3, float param_4, float param_5, float param_6, float param_7, float param_8, float param_9, float param_10, uint param_11, uint param_12);
    public const nint __addr_FUN_008e7d30 = 0x4E7D30;
    private FUN_008e7d30 _FUN_008e7d30;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* TOGetSaveChrName(uint chr_id);
    public const nint __addr_TOGetSaveChrName = 0x4AC800;
    private TOGetSaveChrName _TOGetSaveChrName;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_00905930(byte* name, float param_2, float param_3, byte color, float param_5, int param_6);
    public const nint __addr_FUN_00905930 = 0x505930;
    private FUN_00905930 _FUN_00905930;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate FhLangId TOGetFFXLang();
    public const nint __addr_TOGetFFXLang = 0x4AC2A0;
    private TOGetFFXLang _TOGetFFXLang;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint FUN_008bd9d0(int param_1);
    public const nint __addr_FUN_008bd9d0 = 0x4BD9D0;
    private FUN_008bd9d0 _FUN_008bd9d0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_009055c0(int param_1, float param_2, float param_3, int param_4, float param_5, float param_6);
    public const nint __addr_FUN_009055c0 = 0x5055C0;
    private FUN_009055c0 _FUN_009055c0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte FUN_008a9b30(byte param_1);
    public const nint __addr_FUN_008a9b30 = 0x4A9B30;
    private FUN_008a9b30 _FUN_008a9b30;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_00905230(int param_1, float* param_2, float param_3, float param_4);
    public const nint __addr_FUN_00905230 = 0x505230;
    private FUN_00905230 _FUN_00905230;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_00905550(int param_1, float param_2, float param_3, byte param_4, float param_5);
    public const nint __addr_FUN_00905550 = 0x505550;
    private FUN_00905550 _FUN_00905550;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte FUN_008bda10(byte param_1);
    public const nint __addr_FUN_008bda10 = 0x4BDA10;
    private FUN_008bda10 _FUN_008bda10;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsGetNextAP(int chr_id);
    public const nint __addr_MsGetNextAP = 0x384F50;
    private MsGetNextAP _MsGetNextAP;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_00785370(byte param_1);
    public const nint __addr_FUN_00785370 = 0x385370;
    private FUN_00785370 _FUN_00785370;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_00901660(byte* param_1, float param_2, float param_3, byte param_4, float param_5, float param_6);
    public const nint __addr_FUN_00901660 = 0x501660;
    private FUN_00901660 _FUN_00901660;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TODrawMenuPlateXYWHType(float x, float y, float w, float h, int type);
    public const nint __addr_TODrawMenuPlateXYWHType = 0x4F5F70;
    private TODrawMenuPlateXYWHType _TODrawMenuPlateXYWHType;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void TOMenuDrawKickTmp();
    public const nint __addr_TOMenuDrawKickTmp = 0x4BE9F0;
    private TOMenuDrawKickTmp _TOMenuDrawKickTmp;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_00798be0(BtlRewardData* get_data);
    public const nint __addr_FUN_00798be0 = 0x398BE0;
    private FUN_00798be0 _FUN_00798be0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte MsGetSavePlyJoined(byte idx);
    public const nint __addr_MsGetSavePlyJoined = 0x385460;
    private MsGetSavePlyJoined _MsGetSavePlyJoined;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint Brnd(int param_1);
    public const nint __addr_Brnd = 0x398900;
    private Brnd _Brnd;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int MsCheckRange(int n, int min, int max);
    public const nint __addr_MsCheckRange = 0x39A0D0;
    private MsCheckRange _MsCheckRange;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint FUN_00798aa0(uint param_1);
    public const nint __addr_FUN_00798aa0 = 0x398AA0;
    private FUN_00798aa0 _FUN_00798aa0;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate byte MsWeaponNameNum(Equipment* gear);
    public const nint __addr_MsWeaponNameNum = 0x3A0D10;
    private MsWeaponNameNum _MsWeaponNameNum;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Equipment* MsGetSaveWeapon(uint gear_inv_idx, byte** ref_name);
    public const nint __addr_MsGetSaveWeapon = 0x3ABBF0;
    private MsGetSaveWeapon _MsGetSaveWeapon;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void* TkMn2GetExcelData(int req_elem_idx, ExcelDataFile* excel_data_ptr);
    public const nint __addr_TkMn2GetExcelData = 0x4C1AD0;
    private TkMn2GetExcelData _TkMn2GetExcelData;
    [StructLayout(LayoutKind.Sequential)]
    public struct ExcelDataFile
    {
        public ushort chunk_count;
        private byte __0x02;
        private byte __0x03;
        private byte __0x04;
        private byte __0x05;
        private byte __0x06;
        private byte __0x07;
        public ExcelHeader chunk_headers;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct ExcelHeader
    {
        public ushort first_idx;
        public ushort last_idx;
        public ushort element_size;
        public ushort data_length;
        public nint data_start;

        public readonly int length => last_idx + 1 - first_idx;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void* FUN_008d9140(uint param_1);
    public const nint __addr_FUN_008d9140 = 0x4D9140;
    private FUN_008d9140 _FUN_008d9140;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte FUN_008a9c20(uint param_1);
    public const nint __addr_FUN_008a9c20 = 0x4A9C20;
    private FUN_008a9c20 _FUN_008a9c20;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte FUN_008a97d0(uint param_1);
    public const nint __addr_FUN_008a97d0 = 0x4A97D0;
    private FUN_008a97d0 _FUN_008a97d0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008f8bb0(int param_1, float param_2, float param_3, float param_4, float param_5);
    public const nint __addr_FUN_008f8bb0 = 0x4F8BB0;
    private FUN_008f8bb0 _FUN_008f8bb0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void* FUN_008bee40(uint param_1);
    public const nint __addr_FUN_008bee40 = 0x4BEE40;
    private FUN_008bee40 _FUN_008bee40;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ToGetBtlEasyFontWidth(byte* text, float* ref_width, int param_3, float scale);
    public const nint __addr_ToGetBtlEasyFontWidth = 0x505290;
    private ToGetBtlEasyFontWidth _ToGetBtlEasyFontWidth = FhUtil.get_fptr<ToGetBtlEasyFontWidth>(FhCall.__addr_ToGetBtlEasyFontWidth);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008d8a70(float param_1, float param_2, int param_3);
    public const nint __addr_FUN_008d8a70 = 0x4D8A70;
    private FUN_008d8a70 _FUN_008d8a70;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DrawCrossMenuIconWeaponName2(ushort* param_1, float param_2, float param_3, float param_4);
    public const nint __addr_DrawCrossMenuIconWeaponName2 = 0x4E6970;
    private DrawCrossMenuIconWeaponName2 _DrawCrossMenuIconWeaponName2;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ToMakeBtlEasyFont(byte* param_1, float param_2, float param_3, float param_4, float param_5);
    public const nint __addr_ToMakeBtlEasyFont = 0x505AB0;
    private ToMakeBtlEasyFont _ToMakeBtlEasyFont = FhUtil.get_fptr<ToMakeBtlEasyFont>(FhCall.__addr_ToMakeBtlEasyFont);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsGetRamChrMonster(uint mon_id);
    public const nint __addr_MsGetRamChrMonster = 0x39AF00;
    private MsGetRamChrMonster _MsGetRamChrMonster;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsLimitUp(int param_1, Chr* character, uint init_limit_add);
    public const nint __addr_MsLimitUp = 0x3B15A0;
    private MsLimitUp _MsLimitUp;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Chr* MsGetChr(uint chr_id);
    public const nint __addr_MsGetChr = 0x394030;
    private MsGetChr _MsGetChr;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsCalcWeakLevel(int current_hp, int max_hp);
    public const nint __addr_MsCalcWeakLevel = 0x38BFC0;
    private MsCalcWeakLevel _MsCalcWeakLevel;



    private int TkFont_a => FhUtil.get_at<int>(0x01fcc470);
    private int TkFont_b => FhUtil.get_at<int>(0x01fcc468);
    private int TkFont_g => FhUtil.get_at<int>(0x01fcc460);
    private int TkFont_r => FhUtil.get_at<int>(0x01fcc458);

    private int* p_DAT_01869ee4 => FhUtil.ptr_at<int>(0x01469ee4);

    private int*    p_DAT_01869ee0 => FhUtil.ptr_at<int   >(0x01469ee0);
    private ushort* p_DAT_00c56870 => FhUtil.ptr_at<ushort>(0x00856870);
    private byte*   p_DAT_01869eea => FhUtil.ptr_at<byte  >(0x01469eea);

    private uint DAT_0186ab60                              => FhUtil.get_at<uint>(0x0146ab60);
    private int* p_DAT_0186aadc_curShopIdx                 => FhUtil.ptr_at<int >(0x0146aadc);
    private int* p_DAT_0186ab68_arms_shop_bin_ptr          => FhUtil.ptr_at<int >(0x0146ab68);
    private OverdriveMenu* p_DAT_01597730_OvrModesMenuList => (OverdriveMenu*)FhUtil.ptr_at<uint>(0x01197730);
    [StructLayout(LayoutKind.Sequential)]
    public struct OverdriveMenu
    {
        public ushort overdrive_id;
        public byte type;
        public byte field2_0x3;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Command* MsGetRomPlyCommand(uint com_id, int* param_2);
    public const nint __addr_MsGetRomPlyCommand = 0x390AE0;
    private MsGetRomPlyCommand _MsGetRomPlyCommand;

    private byte* toBwNum => FhUtil.ptr_at<byte>(0x01fcc092);

    private static ushort[] _filteredSummonList = new ushort[9];
    private static ushort* _filteredSummonPtr;
    private static GCHandle _filteredSummonHandle;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate byte TkMenuGetCurrentPlayer();
    public const nint __addr_TkMenuGetCurrentPlayer = 0x4A9810;
    private TkMenuGetCurrentPlayer _TkMenuGetCurrentPlayer;


    // Hooks
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CT_RetInt_0171_restoreCharHp(AtelBasicWorker* work, int* storage, AtelStack* stack);
    public const nint __addr_CT_RetInt_0171_restoreCharHp = 0x45C4F0;
    private FhMethodHandle<CT_RetInt_0171_restoreCharHp> _CT_RetInt_0171_restoreCharHp;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CT_RetInt_0172_restoreCharMp(AtelBasicWorker* work, int* storage, AtelStack* stack);
    public const nint __addr_CT_RetInt_0172_restoreCharMp = 0x45C6B0;
    private FhMethodHandle<CT_RetInt_0172_restoreCharMp> _CT_RetInt_0172_restoreCharMp;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008c0220(uint param_1, float param_2, float param_3, float param_4, float param_5);
    public const nint __addr_FUN_008c0220 = 0x4C0220;
    private FhMethodHandle<FUN_008c0220> _FUN_008c0220;

    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix4f
    {
        public InlineArray4<float> floats0;
        public InlineArray4<int> ints0;
        public InlineArray4<float> floats1;
        public InlineArray4<int> ints1;
        public InlineArray4<float> floats2;
        public InlineArray4<int> ints2;
        public InlineArray4<float> floats3;
        public InlineArray4<int> ints3;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int AtelPushMember(AtelBasicWorker* work, int* storage, AtelStack* stack);
    public const nint __addr_AtelPushMember = 0x46E2A0;
    private FhMethodHandle<AtelPushMember> _AtelPushMember;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_0086dd40(AtelBasicWorker* work, int* storage, AtelStack* stack);
    public const nint __addr_FUN_0086dd40 = 0x46DD40;
    private FhMethodHandle<FUN_0086dd40> _FUN_0086dd40;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008bc300(int param_1);
    public const nint __addr_FUN_008bc300 = 0x4BC300;
    private FhMethodHandle<FUN_008bc300> _FUN_008bc300;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* MsWeaponName(int name_id, int owner, int hiragana, ushort* ref_model_id);
    public const nint __addr_MsWeaponName = 0x3A0C70;
    private FhMethodHandle<MsWeaponName> _MsWeaponName;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsGetItemInternal_00798C20(int param_1, int param_2, int param_3);
    public const nint __addr_MsGetItemInternal_00798C20 = 0x398C20;
    private FhMethodHandle<MsGetItemInternal_00798C20> _MsGetItemInternal_00798C20;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MsChangeWeaponInvisible(uint param_1, byte param_2);
    public const nint __addr_MsChangeWeaponInvisible = 0x3AD5F0;
    private FhMethodHandle<MsChangeWeaponInvisible> _MsChangeWeaponInvisible;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008d85f0(int param_1, int param_2);
    public const nint __addr_FUN_008d85f0 = 0x4D85F0;
    private FhMethodHandle<FUN_008d85f0> _FUN_008d85f0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsLimitTypeDamageCheck(uint param_1, int param_2, uint param_3, int param_4, int param_5, int param_6, int param_7);
    public const nint __addr_MsLimitTypeDamageCheck = 0x3B0D60;
    private FhMethodHandle<MsLimitTypeDamageCheck> _MsLimitTypeDamageCheck;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsLimitTypeDeathCheck(int param_1, int param_2, uint param_3, int param_4);
    public const nint __addr_MsLimitTypeDeathCheck = 0x3B0F90;
    private FhMethodHandle<MsLimitTypeDeathCheck> _MsLimitTypeDeathCheck;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_007b10d0(uint chr_id, uint limit_mode, int param_3);
    public const nint __addr_FUN_007b10d0 = 0x3B10D0;
    private FhMethodHandle<FUN_007b10d0> _FUN_007b10d0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsLimitTypeTurnCheck(uint param_1, int param_2);
    public const nint __addr_MsLimitTypeTurnCheck = 0x3B13D0;
    private FhMethodHandle<MsLimitTypeTurnCheck> _MsLimitTypeTurnCheck;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int MsLimitTypeWinCheck();
    public const nint __addr_MsLimitTypeWinCheck = 0x3B1550;
    private FhMethodHandle<MsLimitTypeWinCheck> _MsLimitTypeWinCheck;

    // Seymour Specific
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void MsSetSaveStartGame();
    public const nint __addr_MsSetSaveStartGame = 0x386BC0;
    private FhMethodHandle<MsSetSaveStartGame> _MsSetSaveStartGame;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_00635c20(uint param_1);
    public const nint __addr_FUN_00635c20 = 0x235C20;
    private FhMethodHandle<FUN_00635c20> _FUN_00635c20;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsParseCommand(byte* param_1);
    public const nint __addr_MsParseCommand = 0x3AE380;
    private FhMethodHandle<MsParseCommand> _MsParseCommand;

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate void TOBtlCtrlHelpWin(int param_1);
    public const nint __addr_TOBtlCtrlHelpWin = 0x491250;
    private FhMethodHandle<TOBtlCtrlHelpWin> _TOBtlCtrlHelpWin;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort* TOGetSaveWindow(uint chr_id, BtlWindowType window_type, int* summonlistlength);
    public const nint __addr_TOGetSaveWindow = 0x49B510;
    private FhMethodHandle<TOGetSaveWindow> _TOGetSaveWindow;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int TkMenuSummonEnableMask();
    public const nint __addr_TkMenuSummonEnableMask = 0x4AB190;
    private FhMethodHandle<TkMenuSummonEnableMask> _TkMenuSummonEnableMask;



    private ushort* _NkSeymourLegend = FhUtil.ptr_at<ushort>(0x886D80);



    public void init_fptrs()
    {
        _AtelPopStackInteger = FhUtil.get_fptr<AtelPopStackInteger>(__addr_AtelPopStackInteger);
        _MsGetSavePlayerPtr = FhUtil.get_fptr<MsGetSavePlayerPtr>(__addr_MsGetSavePlayerPtr);
        _TOGetShapTextureName = FhUtil.get_fptr<TOGetShapTextureName>(__addr_TOGetShapTextureName);
        _TOGetImageWH = FhUtil.get_fptr<TOGetImageWH>(__addr_TOGetImageWH);
        _graphicDrawUIElement = FhUtil.get_fptr<graphicDrawUIElement>(__addr_graphicDrawUIElement);
        _AtelGetAlbhedRikku = FhUtil.get_fptr<AtelGetAlbhedRikku>(__addr_AtelGetAlbhedRikku);
        _AtelGetEventSaveRamAdrs = FhUtil.get_fptr<MsGetSaveEventAddress>(__addr_AtelGetEventSaveRamAdrs);
        _MsGetSavePartyMember = FhUtil.get_fptr<MsGetSavePartyMember>(__addr_MsGetSavePartyMember);
        _MsGetSavePlyJoin = FhUtil.get_fptr<MsGetSavePlyJoin>(__addr_MsGetSavePlyJoin);
        _MsSetSavePlyJoin = FhUtil.get_fptr<MsSetSavePlyJoin>(__addr_MsSetSavePlyJoin);
        _FUN_00786a10 = FhUtil.get_fptr<FUN_00786a10>(__addr_FUN_00786a10);
        _FUN_0088e6c0 = FhUtil.get_fptr<FUN_0088e6c0>(__addr_FUN_0088e6c0);
        _FUN_0088e6a0 = FhUtil.get_fptr<FUN_0088e6a0>(__addr_FUN_0088e6a0);
        _TOMenuOpenPktBuffTmp = FhUtil.get_fptr<TOMenuOpenPktBuffTmp>(__addr_TOMenuOpenPktBuffTmp);
        _graphicUiRemapX2 = FhUtil.get_fptr<graphicUiRemapX2>(__addr_graphicUiRemapX2);
        _graphicUiRemapY2 = FhUtil.get_fptr<graphicUiRemapY2>(__addr_graphicUiRemapY2);
        _TOMkpShapeXYWHUV = FhUtil.get_fptr<TOMkpShapeXYWHUV>(__addr_TOMkpShapeXYWHUV);
        _FUN_008e7d30 = FhUtil.get_fptr<FUN_008e7d30>(__addr_FUN_008e7d30);
        _TOGetSaveChrName = FhUtil.get_fptr<TOGetSaveChrName>(__addr_TOGetSaveChrName);
        _FUN_00905930 = FhUtil.get_fptr<FUN_00905930>(__addr_FUN_00905930);
        _TOGetFFXLang = FhUtil.get_fptr<TOGetFFXLang>(__addr_TOGetFFXLang);
        _FUN_008bd9d0 = FhUtil.get_fptr<FUN_008bd9d0>(__addr_FUN_008bd9d0);
        _FUN_009055c0 = FhUtil.get_fptr<FUN_009055c0>(__addr_FUN_009055c0);
        _FUN_008a9b30 = FhUtil.get_fptr<FUN_008a9b30>(__addr_FUN_008a9b30);
        _FUN_00905230 = FhUtil.get_fptr<FUN_00905230>(__addr_FUN_00905230);
        _FUN_00905550 = FhUtil.get_fptr<FUN_00905550>(__addr_FUN_00905550);
        _FUN_008bda10 = FhUtil.get_fptr<FUN_008bda10>(__addr_FUN_008bda10);
        _MsGetNextAP = FhUtil.get_fptr<MsGetNextAP>(__addr_MsGetNextAP);
        _FUN_00785370 = FhUtil.get_fptr<FUN_00785370>(__addr_FUN_00785370);
        _FUN_00901660 = FhUtil.get_fptr<FUN_00901660>(__addr_FUN_00901660);
        _TODrawMenuPlateXYWHType = FhUtil.get_fptr<TODrawMenuPlateXYWHType>(__addr_TODrawMenuPlateXYWHType);
        _TOMenuDrawKickTmp = FhUtil.get_fptr<TOMenuDrawKickTmp>(__addr_TOMenuDrawKickTmp);
        _FUN_00798be0 = FhUtil.get_fptr<FUN_00798be0>(__addr_FUN_00798be0);
        _MsGetSavePlyJoined = FhUtil.get_fptr<MsGetSavePlyJoined>(__addr_MsGetSavePlyJoined);
        _Brnd = FhUtil.get_fptr<Brnd>(__addr_Brnd);
        _MsCheckRange = FhUtil.get_fptr<MsCheckRange>(__addr_MsCheckRange);
        _FUN_00798aa0 = FhUtil.get_fptr<FUN_00798aa0>(__addr_FUN_00798aa0);
        _MsWeaponNameNum = FhUtil.get_fptr<MsWeaponNameNum>(__addr_MsWeaponNameNum);
        _MsGetSaveWeapon = FhUtil.get_fptr<MsGetSaveWeapon>(__addr_MsGetSaveWeapon);
        _TkMn2GetExcelData = FhUtil.get_fptr<TkMn2GetExcelData>(__addr_TkMn2GetExcelData);
        _FUN_008d9140 = FhUtil.get_fptr<FUN_008d9140>(__addr_FUN_008d9140);
        _FUN_008a9c20 = FhUtil.get_fptr<FUN_008a9c20>(__addr_FUN_008a9c20);
        _FUN_008a97d0 = FhUtil.get_fptr<FUN_008a97d0>(__addr_FUN_008a97d0);
        _FUN_008f8bb0 = FhUtil.get_fptr<FUN_008f8bb0>(__addr_FUN_008f8bb0);
        _FUN_008bee40 = FhUtil.get_fptr<FUN_008bee40>(__addr_FUN_008bee40);
        _ToGetBtlEasyFontWidth = FhUtil.get_fptr<ToGetBtlEasyFontWidth>(__addr_ToGetBtlEasyFontWidth);
        _FUN_008d8a70 = FhUtil.get_fptr<FUN_008d8a70>(__addr_FUN_008d8a70);
        _DrawCrossMenuIconWeaponName2 = FhUtil.get_fptr<DrawCrossMenuIconWeaponName2>(__addr_DrawCrossMenuIconWeaponName2);
        _ToMakeBtlEasyFont = FhUtil.get_fptr<ToMakeBtlEasyFont>(__addr_ToMakeBtlEasyFont);
        _MsGetRamChrMonster = FhUtil.get_fptr<MsGetRamChrMonster>(__addr_MsGetRamChrMonster);
        _MsLimitUp = FhUtil.get_fptr<MsLimitUp>(__addr_MsLimitUp);
        _MsGetChr = FhUtil.get_fptr<MsGetChr>(__addr_MsGetChr);
        _MsCalcWeakLevel = FhUtil.get_fptr<MsCalcWeakLevel>(__addr_MsCalcWeakLevel);
        _MsGetRomPlyCommand = FhUtil.get_fptr<MsGetRomPlyCommand>(__addr_MsGetRomPlyCommand);
        _TkMenuGetCurrentPlayer = FhUtil.get_fptr<TkMenuGetCurrentPlayer>(__addr_TkMenuGetCurrentPlayer);
    }

    public override bool init(FhModContext mod_context, FileStream global_state_file) 
    {
        _CT_RetInt_0171_restoreCharHp = new FhMethodHandle<CT_RetInt_0171_restoreCharHp>(this, game, __addr_CT_RetInt_0171_restoreCharHp, h_CT_RetInt_0171_restoreCharHp);
        _CT_RetInt_0172_restoreCharMp = new FhMethodHandle<CT_RetInt_0172_restoreCharMp>(this, game, __addr_CT_RetInt_0172_restoreCharMp, h_CT_RetInt_0172_restoreCharMp);
        _FUN_008c0220 = new FhMethodHandle<FUN_008c0220>(this, game, __addr_FUN_008c0220, h_FUN_008c0220);
        _AtelPushMember = new FhMethodHandle<AtelPushMember>(this, game, __addr_AtelPushMember, h_AtelPushMember);
        _FUN_0086dd40 = new FhMethodHandle<FUN_0086dd40>(this, game, __addr_FUN_0086dd40, h_FUN_0086dd40);
        _FUN_008bc300 = new FhMethodHandle<FUN_008bc300>(this, game, __addr_FUN_008bc300, h_FUN_008bc300);
        _MsWeaponName = new FhMethodHandle<MsWeaponName>(this, game, __addr_MsWeaponName, h_MsWeaponName);
        _MsGetItemInternal_00798C20 = new FhMethodHandle<MsGetItemInternal_00798C20>(this, game, __addr_MsGetItemInternal_00798C20, h_MsGetItemInternal_00798C20);
        _MsChangeWeaponInvisible = new FhMethodHandle<MsChangeWeaponInvisible>(this, game, __addr_MsChangeWeaponInvisible, h_MsChangeWeaponInvisible);
        _FUN_008d85f0 = new FhMethodHandle<FUN_008d85f0>(this, game, __addr_FUN_008d85f0, h_FUN_008d85f0);
        _MsLimitTypeDamageCheck = new FhMethodHandle<MsLimitTypeDamageCheck>(this, game, __addr_MsLimitTypeDamageCheck, h_MsLimitTypeDamageCheck);
        _MsLimitTypeDeathCheck = new FhMethodHandle<MsLimitTypeDeathCheck>(this, game, __addr_MsLimitTypeDeathCheck, h_MsLimitTypeDeathCheck);
        _FUN_007b10d0 = new FhMethodHandle<FUN_007b10d0>(this, game, __addr_FUN_007b10d0, h_FUN_007b10d0);
        _MsLimitTypeTurnCheck = new FhMethodHandle<MsLimitTypeTurnCheck>(this, game, __addr_MsLimitTypeTurnCheck, h_MsLimitTypeTurnCheck);
        _MsLimitTypeWinCheck = new FhMethodHandle<MsLimitTypeWinCheck>(this, game, __addr_MsLimitTypeWinCheck, h_MsLimitTypeWinCheck);

        // Seymour Specific
        _MsSetSaveStartGame = new FhMethodHandle<MsSetSaveStartGame>(this, game, __addr_MsSetSaveStartGame, h_MsSetSaveStartGame);
        _FUN_00635c20 = new FhMethodHandle<FUN_00635c20>(this, game, __addr_FUN_00635c20, h_FUN_00635c20);
        _MsParseCommand = new FhMethodHandle<MsParseCommand>(this, game, __addr_MsParseCommand, h_MsParseCommand);
        _TOBtlCtrlHelpWin = new FhMethodHandle<TOBtlCtrlHelpWin>(this, game, __addr_TOBtlCtrlHelpWin, h_TOBtlCtrlHelpWin);
        _TOGetSaveWindow = new FhMethodHandle<TOGetSaveWindow>(this, game, __addr_TOGetSaveWindow, h_TOGetSaveWindow);
        _TkMenuSummonEnableMask = new FhMethodHandle<TkMenuSummonEnableMask>(this, game, __addr_TkMenuSummonEnableMask, h_TkMenuSummonEnableMask);

        init_fptrs();

        _NkSeymourLegend[0] = 0x8019; // Break Damage Limit
        _NkSeymourLegend[1] = 0x800F; // Triple Overdrive
        _NkSeymourLegend[2] = 0x8006; // Magic Booster
        _NkSeymourLegend[3] = 0x8018; // Break MP Limit

        return _CT_RetInt_0171_restoreCharHp.hook() &&
               _CT_RetInt_0172_restoreCharMp.hook() &&
               _FUN_008c0220.hook() &&
               _AtelPushMember.hook() &&
               _FUN_0086dd40.hook() &&
               _FUN_008bc300.hook() &&
               _MsWeaponName.hook() &&
               _MsGetItemInternal_00798C20.hook() &&
               _MsChangeWeaponInvisible.hook() &&
               _FUN_008d85f0.hook() &&
               _MsLimitTypeDamageCheck.hook() &&
               _MsLimitTypeDeathCheck.hook() &&
               _FUN_007b10d0.hook() &&
               _MsLimitTypeTurnCheck.hook() &&
               _MsLimitTypeWinCheck.hook() &&
               _MsSetSaveStartGame.hook() &&
               _FUN_00635c20.hook() &&
               _MsParseCommand.hook() &&
               _TOBtlCtrlHelpWin.hook() &&
               _TOGetSaveWindow.hook() &&
               _TkMenuSummonEnableMask.hook();
    }

    public override void load_local_state(FileStream? local_state_file, FhLocalStateInfo local_state_info) { }
    public override void save_local_state(FileStream  local_state_file)                                    { }

    int h_CT_RetInt_0171_restoreCharHp(AtelBasicWorker* work, int* storage, AtelStack* stack)
    {
        uint chr_id;
        PlySave* ply_save;

        chr_id = (uint)_AtelPopStackInteger(work, stack);
        ply_save = _MsGetSavePlayerPtr(chr_id);
        ply_save->__0x3D = 0;
        if (ply_save->max_hp < ply_save->hp)
        {
            return (int)ply_save->hp;
        }
        ply_save->hp = ply_save->max_hp;
        if (chr_id == 3)
        {
            PlySave* seymour = _MsGetSavePlayerPtr(7);
            seymour->__0x3D = 0;
            seymour->hp = seymour->max_hp;
        }
        return (int)ply_save->max_hp;
    }

    int h_CT_RetInt_0172_restoreCharMp(AtelBasicWorker* work, int* storage, AtelStack* stack)
    {
        uint chr_id;
        PlySave* ply_save;

        chr_id = (uint)_AtelPopStackInteger(work, stack);
        ply_save = _MsGetSavePlayerPtr(chr_id);
        if (ply_save->max_mp < ply_save->mp)
        {
            return (int)ply_save->mp;
        }
        ply_save->mp = ply_save->max_mp;
        if (chr_id == 3)
        {
            PlySave* seymour = _MsGetSavePlayerPtr(7);
            seymour->mp = seymour->max_mp;
        }
        return (int)ply_save->max_mp;
    }

    // Portrait in menus
    void h_FUN_008c0220(uint param_1, float param_2, float param_3, float param_4, float param_5)
    {
        float fVar1;
        float fVar2;
        float fVar3;
        float fVar4;
        int iVar5;
        byte* pcVar6;
        byte* tex_path;
        uint uVar7;
        float local_b0;
        float local_ac;
        float local_a8;
        byte* local_a4;
        Matrix4f local_a0 = new();
        //undefined4 local_10;
        //undefined4 local_c;
        //uint local_8;
        float newVar;

        fVar4 = TkFont_a;
        fVar3 = TkFont_b;
        fVar2 = TkFont_g;
        fVar1 = TkFont_r;
        //local_8 = __security_cookie ^ (uint)&stack0xfffffffc;
        //local_10 = 0;
        //local_c = 0;
        if (param_1 < 8)
        {
            local_a4 = _TOGetShapTextureName(0x2ed0);
            _TOGetImageWH(0x2ed0, &local_ac, &local_b0);
            if (param_1 == 7)
            {
                param_1 = 8;
                uVar7 = 4;
                pcVar6 = (byte*)100;
                local_a0.floats0[0] = param_2;
                local_a0.floats0[1] = param_3;
                local_a0.floats0[2] = (float)(uVar7 * 100) / local_ac;
                local_a0.floats0[3] = (float)(int)pcVar6 / local_b0;
                local_a0.ints0[0] = (int)fVar4; local_a0.ints0[1] = (int)fVar3;
                local_a0.ints0[2] = (int)fVar2; local_a0.ints0[3] = (int)fVar1;
                local_a0.floats1[0] = param_4 + param_2;
                local_a0.floats1[1] = param_5 + param_3;
                local_a0.floats1[2] = (float)(uVar7 * 100 + 100) / local_ac;
                local_a0.floats1[3] = (float)((int)pcVar6 + 100) / local_b0;
                local_a0.ints1[0] = (int)fVar4; local_a0.ints1[1] = (int)fVar3;
                local_a0.ints1[2] = (int)fVar2; local_a0.ints1[3] = (int)fVar1;
                _graphicDrawUIElement(&local_a0, local_a4, 1, 0, 0);
                return;
            }
            else
            {
                if (param_1 == 6)
                {
                    iVar5 = _AtelGetAlbhedRikku();
                    if (iVar5 == 1) param_1 = 7;
                }
                uVar7 = param_1 & 0x80000003;
                if ((int)uVar7 < 0)
                {
                    uVar7 = (uVar7 - 1 | 0xfffffffc) + 1;
                }
            }
            local_a0.floats0[0] = param_2;
            local_a0.floats0[1] = param_3;
            local_a0.floats0[2] = (float)(int)(uVar7 * 100) / local_ac;
            pcVar6 = (byte*)(((int)param_1 >> 2) * 100);
            local_a0.floats0[3] = (float)(int)pcVar6 / local_b0;
            local_a0.ints0[0] = (int)fVar4;
            local_a0.ints0[1] = (int)fVar3;
            local_a0.ints0[2] = (int)fVar2;
            local_a0.ints0[3] = (int)fVar1;
            local_a0.floats1[0] = param_4 + param_2;
            local_a0.floats1[1] = param_3 + param_5;
            local_a0.floats1[2] = (float)(int)(uVar7 * 100 + 100) / local_ac;
            local_a8 = (float)(int)(pcVar6 + 100);
            local_a0.floats1[3] = local_a8 / local_b0;
            local_a0.ints1[0] = (int)fVar4;
            local_a0.ints1[1] = (int)fVar3;
            local_a0.ints1[2] = (int)fVar2;
            local_a0.ints1[3] = (int)fVar1;
            _graphicDrawUIElement(&local_a0, local_a4, 1, 0, 0);
            return;
        }
        pcVar6 = _TOGetShapTextureName(0x2ed4);
        _TOGetImageWH(0x2ed4, &local_ac, &local_b0);
        local_a0.floats0[0] = param_2;
        local_a0.floats0[1] = param_3;
        tex_path = (byte*)(((int)(param_1 - 8) / 5) * 100);
        iVar5 = ((int)(param_1 - 8) % 5) * 100;
        local_a0.floats0[2] = (float)iVar5 / local_ac;
        local_a0.floats0[3] = (float)(int)tex_path / local_b0;
        local_a0.ints0[0] = (int)fVar4;
        local_a0.ints0[1] = (int)fVar3;
        local_a0.ints0[2] = (int)fVar2;
        local_a0.ints0[3] = (int)fVar1;
        local_a0.floats1[0] = param_4 + param_2;
        local_a0.floats1[1] = param_3 + param_5;
        local_a0.floats1[2] = (float)(iVar5 + 100) / local_ac;
        newVar = (float)((int)tex_path + 100);
        local_a0.floats1[3] = newVar / local_b0;
        local_a0.ints1[0] = (int)fVar4;
        local_a0.ints1[1] = (int)fVar3;
        local_a0.ints1[2] = (int)fVar2;
        local_a0.ints1[3] = (int)fVar1;
        _graphicDrawUIElement(&local_a0, pcVar6, 1, 0, 0);
        return;
    }

    // Save Party
    int h_AtelPushMember(AtelBasicWorker* work, int* storage, AtelStack* stack)
    {
        byte bVar1;
        SaveData* pSVar2;
        BOOL BVar3;
        byte* puVar4;
        uint* puVar5;
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
        do
        {
            if (*puVar5 == 0xff)
            {
                *puVar4 = 0xff;
            }
            else
            {
                *puVar4 = (byte)*puVar5;
            }
            puVar5 = puVar5 + 1;
            puVar4 = puVar4 + 1;
            iVar6 = iVar6 + -1;
        } while (iVar6 != 0);
        chr_id = 0;
        *(int*)&pSVar2->atel_push_party = 0;
        uVar7 = 1;
        do
        {
            BVar3 = _MsGetSavePlyJoin(chr_id);
            if (BVar3 == 1)
            {
                *(uint*)&pSVar2->atel_push_party = *(uint*)&pSVar2->atel_push_party | uVar7;
            }
            uVar7 = uVar7 << 1 | (uint)((int)uVar7 < 0 ? 1 : 0);
            chr_id = chr_id + 1;
        } while ((int)chr_id < 8);
        pSVar2->atel_is_push_member = 1;
        return bVar1;
    }

    // Restore Party
    int h_FUN_0086dd40(AtelBasicWorker* work, int* storage, AtelStack* stack)
    {
        byte bVar1;
        SaveData* pSVar2;
        uint uVar3;
        uint uVar4;
        uint uVar5;

        pSVar2 = _AtelGetEventSaveRamAdrs();
        bVar1 = pSVar2->atel_is_push_member;
        if (bVar1 != 0)
        {
            uVar4 = 0;
            uVar5 = 1;
            do
            {
                _MsSetSavePlyJoin(uVar4, (int)(((*(uint*)&pSVar2->atel_push_party & uVar5) != 0) ? 1u : 0u));
                uVar5 = uVar5 << 1 | (((int)uVar5 < 0) ? 1u : 0u);
                uVar4 = uVar4 + 1;
            } while ((int)uVar4 < 8);
            uVar4 = pSVar2->atel_push_frontline[0];
            if (pSVar2->atel_push_frontline[0] == 0xff)
            {
                uVar4 = 0xff;
            }
            uVar5 = pSVar2->atel_push_frontline[1];
            if (pSVar2->atel_push_frontline[1] == 0xff)
            {
                uVar5 = 0xff;
            }
            uVar3 = pSVar2->atel_push_frontline[2];
            if (pSVar2->atel_push_frontline[2] == 0xff)
            {
                uVar3 = 0xff;
            }
            _FUN_00786a10(uVar4, uVar5, uVar3);
        }
        pSVar2->atel_is_push_member = 0;
        return bVar1;
    }

    // Battle Results Screen
    void h_FUN_008bc300(int param_1)
    {
        byte* name;
        FhLangId LVar1;
        int iVar2;
        int iVar3;
        float fVar4;
        float fVar5;
        float fVar6;
        float fVar7;
        int uVar8;
        float fVar9;
        float fVar10;
        float fVar11;
        float uVar12;
        byte bVar13;
        float uVar14;
        float* pfVar15;
        float fVar16;
        byte* puVar17;
        uint uVar18;
        float local_2c;
        double local_28;
        float local_20;
        float local_1c;
        float local_18;
        float local_14;
        float local_10;
        float local_c;
        float local_8;

        local_20 = 0.0f;
        local_14 = 0.0f;
        local_10 = 0.0f;
        local_1c = 0.0f;
        local_18 = 0.0f;
        local_c = 0.0f;
        switch (*(short*)((int)p_DAT_01869ee4 + param_1 * 0xe))
        {
            case 0:
            case 5:
                goto switchD_008bc339_caseD_0;
            case 1:
                uVar18 = (uint)_FUN_0088e6c0(*(short*)((int)p_DAT_01869ee0 + param_1 * 0xe + 2));
                iVar2 = (int)((uVar18 & 0xffff) * -0x200);
                goto LAB_008bc382;
            case 2:
            case 3:
                local_c = 0.0f;
                break;
            case 4:
                uVar18 = (uint)_FUN_0088e6a0(0x1000 - *(short*)((int)p_DAT_01869ee0 + param_1 * 0xe + 2));
                iVar2 = (int)(((uVar18 & 0xffff) - 0x1000) * 0x200);
            LAB_008bc382:
                local_c = (int)(iVar2 + (iVar2 >> 0x1f & 0xfffU)) >> 0xc;
                break;
        }
        _TOMenuOpenPktBuffTmp();
        fVar4 = _graphicUiRemapX2(210.0f);
        local_c = fVar4 + local_c;
        local_8 = param_1 * 0x50 + 0x10c;
        local_8 = _graphicUiRemapY2(local_8);
        if (param_1 == 7)
        {
            fVar4 = 6 * 86.0f + 72.0f;
        }
        else
        {
            fVar4 = param_1 * 86.0f + 72.0f;
        }
        fVar16 = 1110.0f;
        if (param_1 == 7)
        {
            fVar7 = 6 * 86.0f + 0.0f;
        }
        else
        {
            fVar7 = param_1 * 86.0f + 0.0f;
        }
        fVar11 = 0.0f;
        fVar5 = _graphicUiRemapY2(72.0f);
        fVar6 = _graphicUiRemapX2(1110.0f);
        _TOMkpShapeXYWHUV(-3, local_c, local_8, fVar6, fVar5, fVar11, fVar7, fVar16, fVar4);
        uVar18 = 0x1ac56870;
        puVar17 = (byte*)p_DAT_00c56870;
        fVar4 = (param_1 + 1) * 64.0f;
        uVar14 = -20.0f;
        uVar12 = 50.0f;
        fVar16 = 64.0f;
        fVar11 = 250.0f;
        uVar8 = 0;
        fVar7 = fVar4;
        fVar5 = _graphicUiRemapY2(64.0f);
        fVar6 = _graphicUiRemapX2(250.0f);
        _FUN_008e7d30(local_c, local_8, fVar6, fVar5, uVar8, fVar7, fVar11, fVar16, uVar12, uVar14, (uint)puVar17, uVar18);
        puVar17 = (byte*)p_DAT_00c56870;
        uVar18 = 0x1ac56870;
        uVar14 = -20.0f;
        uVar12 = 50.0f;
        fVar10 = 64.0f;
        fVar9 = 250.0f;
        uVar8 = (int)250.0f;
        fVar5 = fVar4;
        fVar6 = _graphicUiRemapY2(64.0f);
        fVar11 = _graphicUiRemapX2(250.0f);
        fVar7 = local_8;
        fVar16 = _graphicUiRemapX2(250.0f);
        local_28 = (double)(fVar16 + local_c);
        _FUN_008e7d30(fVar16 + local_c, fVar7, fVar11, fVar6, uVar8, fVar5, fVar9, fVar10, uVar12, uVar14, uVar18, (uint)puVar17);
        uVar18 = 0x33c56870;
        puVar17 = (byte*)p_DAT_00c56870;
        uVar14 = -10.0f;
        uVar12 = -80.0f;
        fVar16 = 64.0f;
        fVar11 = 250.0f;
        uVar8 = (int)300.0f;
        fVar7 = fVar4;
        fVar5 = _graphicUiRemapY2(64.0f);
        fVar6 = _graphicUiRemapX2(250.0f);
        _FUN_008e7d30(local_c, local_8, fVar6, fVar5, uVar8, fVar7, fVar11, fVar16, uVar12, uVar14, (uint)puVar17, uVar18);
        puVar17 = (byte*)p_DAT_00c56870;
        uVar18 = 0x33c56870;
        uVar14 = -10.0f;
        uVar12 = -80.0f;
        fVar9 = 64.0f;
        fVar16 = 250.0f;
        uVar8 = (int)550.0f;
        fVar5 = _graphicUiRemapY2(64.0f);
        fVar6 = _graphicUiRemapX2(250.0f);
        fVar7 = local_8;
        fVar11 = _graphicUiRemapX2(250.0f);
        _FUN_008e7d30(fVar11 + local_c, fVar7, fVar6, fVar5, uVar8, fVar4, fVar16, fVar9, uVar12, uVar14, uVar18, (uint)puVar17);
        fVar10 = 688.0f;
        fVar9 = 1104.0f;
        fVar16 = 618.0f;
        fVar11 = 689.0f;
        fVar7 = _graphicUiRemapY2(70.0f);
        fVar5 = _graphicUiRemapX2(415.0f);
        fVar4 = local_8;
        fVar6 = _graphicUiRemapX2(1090.0f);
        _TOMkpShapeXYWHUV(-3, fVar6 + local_c, fVar4, fVar5, fVar7, fVar11, fVar16, fVar9, fVar10);
        uVar8 = 0x3f800000;
        fVar5 = 0.82f;
        bVar13 = 0;
        fVar4 = _graphicUiRemapY2(9.0f);
        fVar4 = fVar4 + local_8;
        fVar7 = _graphicUiRemapX2(160.0f);
        fVar7 = fVar7 + local_c;
        name = _TOGetSaveChrName((uint)(int)*(short*)((int)p_DAT_01869ee4 + param_1 * 0xe + 2));
        _FUN_00905930(name, fVar7, fVar4, bVar13, fVar5, uVar8);
        LVar1 = _TOGetFFXLang();
        if (LVar1 == FhLangId.Debug)
        {
            local_20 = (float)-20.0;
        }
        LVar1 = _TOGetFFXLang();
        if (LVar1 == FhLangId.Debug)
        {
            local_14 = -35.0f;
            local_10 = 20.0f;
            local_1c = -10.0f;
            local_18 = 25.0f;
        }
        fVar10 = 0.10253906f;
        fVar4 = (local_10 + 434.0f) * 0.0009765625f;
        fVar9 = 0.07519531f;
        fVar16 = 0.37304688f;
        fVar5 = _graphicUiRemapY2(28.0f);
        fVar6 = _graphicUiRemapX2(local_10 + 55.0f);
        fVar7 = _graphicUiRemapY2(33.0f);
        fVar7 = fVar7 + local_8;
        fVar11 = _graphicUiRemapX2(local_20 + 758.0f);
        _TOMkpShapeXYWHUV(200, fVar11 + local_c, fVar7, fVar6, fVar5, fVar16, fVar9, fVar4, fVar10);
        uVar12 = 0;
        fVar6 = 0.7f;
        uVar8 = 0x25;
        fVar4 = _graphicUiRemapY2(27.0f);
        fVar4 = fVar4 + local_8;
        fVar7 = _graphicUiRemapX2(local_20 + 748.0f);
        fVar7 = fVar7 + local_c;
        fVar5 = _FUN_008bd9d0(*(short*)((int)p_DAT_01869ee4 + param_1 * 0xe + 2));
        _FUN_009055c0((int)fVar5, fVar7, fVar4, uVar8, fVar6, uVar12);
        fVar10 = 0.15234375f;
        fVar9 = 0.7421875f;
        fVar16 = 0.12402344f;
        fVar11 = 0.3671875f;
        fVar7 = _graphicUiRemapY2(28.0f);
        fVar5 = _graphicUiRemapX2(384.0f);
        fVar4 = _graphicUiRemapY2(2.0f);
        fVar4 = fVar4 + local_8;
        fVar6 = _graphicUiRemapX2(1096.0f);
        _TOMkpShapeXYWHUV(200, fVar6 + local_c, fVar4, fVar5, fVar7, fVar11, fVar16, fVar9, fVar10);
        fVar10 = 636.0f;
        fVar9 = 208.0f;
        fVar16 = 603.0f;
        fVar11 = 0.0f;
        fVar7 = _graphicUiRemapY2(33.0f);
        fVar5 = _graphicUiRemapX2(208.0f);
        fVar4 = _graphicUiRemapY2(29.0f);
        fVar4 = fVar4 + local_8;
        fVar6 = _graphicUiRemapX2(844.0f);
        _TOMkpShapeXYWHUV(-1, fVar6 + local_c, fVar4, fVar5, fVar7, fVar11, fVar16, fVar9, fVar10);
        fVar10 = 0.6621094f;
        fVar9 = 0.5595703f;
        fVar16 = 0.5888672f;
        fVar11 = 0.48632813f;
        fVar7 = _graphicUiRemapY2(75.0f);
        fVar5 = _graphicUiRemapX2(75.0f);
        local_28 = local_8;
        fVar4 = _graphicUiRemapY2(3.0f);
        fVar4 = (float)local_28 - fVar4;
        fVar6 = _graphicUiRemapX2(1038.0f);
        _TOMkpShapeXYWHUV(0x3d, fVar6 + local_c, fVar4, fVar5, fVar7, fVar11, fVar16, fVar9, fVar10);
        LVar1 = _TOGetFFXLang();
        if (LVar1 == FhLangId.Debug)
        {
            local_14 = -35.0f;
            local_10 = 20.0f;
            local_1c = -10.0f;
            local_18 = 25.0f;
        }
        fVar10 = 0.11328125f;
        fVar4 = (local_10 + 978.0f) * 0.0009765625f;
        fVar9 = 0.08203125f;
        fVar7 = (local_14 + 834.0f) * 0.0009765625f;
        fVar6 = _graphicUiRemapY2(32.0f);
        fVar11 = _graphicUiRemapX2(local_18 + 144.0f);
        fVar5 = _graphicUiRemapY2(32.0f);
        fVar5 = fVar5 + local_8;
        fVar16 = _graphicUiRemapX2(local_1c + 935.0f);
        _TOMkpShapeXYWHUV(0x3d, fVar16 + local_c, fVar5, fVar11, fVar6, fVar7, fVar9, fVar4, fVar10);
        pfVar15 = &local_2c;
        iVar2 = _FUN_008a9b30((byte)*(ushort*)((int)p_DAT_01869ee4 + param_1 * 0xe + 2));
        _FUN_00905230(iVar2, pfVar15, 0.7f, 0.0f);
        fVar5 = 0.7f;
        bVar13 = 0;
        fVar4 = _graphicUiRemapY2(16.0f);
        fVar4 = fVar4 + local_8;
        fVar7 = _graphicUiRemapX2(1076.0f);
        fVar7 = (fVar7 + local_c - local_2c * 0.5f);
        iVar2 = _FUN_008a9b30((byte)*(ushort*)((int)p_DAT_01869ee4 + param_1 * 0xe + 2));
        _FUN_00905550(iVar2, fVar7, fVar4, bVar13, fVar5);
        LVar1 = _TOGetFFXLang();
        if (LVar1 == FhLangId.Debug)
        {
            local_14 = -35.0f;
            local_10 = 20.0f;
            local_1c = -10.0f;
            local_18 = 25.0f;
        }
        fVar10 = 0.10253906f;
        fVar4 = (local_10 + 434.0f) * 0.0009765625f;
        fVar9 = 0.07519531f;
        fVar16 = 0.37304688f;
        fVar5 = _graphicUiRemapY2(28.0f);
        fVar6 = _graphicUiRemapX2(local_10 + 55.0f);
        fVar7 = _graphicUiRemapY2(34.0f);
        fVar7 = fVar7 + local_8;
        fVar11 = _graphicUiRemapX2(1348.0f);
        _TOMkpShapeXYWHUV(200, fVar11 + local_c, fVar7, fVar6, fVar5, fVar16, fVar9, fVar4, fVar10);
        iVar2 = _FUN_008bda10((byte)*(ushort*)((int)p_DAT_01869ee4 + param_1 * 0xe + 2));
        fVar7 = 0.0f;
        fVar4 = 0.7f;
        uVar8 = 0x25;
        if (iVar2 < 99)
        {
            fVar5 = _graphicUiRemapY2(28.0f);
            fVar5 = fVar5 + local_8;
            fVar6 = _graphicUiRemapX2(1333.0f);
            fVar6 = fVar6 + local_c;
            iVar2 = _MsGetNextAP(*(short*)((int)p_DAT_01869ee4 + param_1 * 0xe + 2));
            iVar3 = _FUN_00785370((byte)*(short*)((int)p_DAT_01869ee4 + param_1 * 0xe + 2));
            _FUN_009055c0(iVar2 - iVar3, fVar6, fVar5, uVar8, fVar4, fVar7);
        }
        else
        {
            fVar5 = _graphicUiRemapY2(38.0f);
            bVar13 = (byte)uVar8;
            fVar5 = fVar5 + local_8;
            fVar6 = _graphicUiRemapX2(1210.0f);
            _FUN_00901660(textString, fVar6 + local_c, fVar5, bVar13, fVar4, fVar7);
        }
        if ('\0' < p_DAT_01869eea[param_1 * 0xe])
        {
            uVar18 = (uint)(0xf - p_DAT_01869eea[param_1 * 0xe]);
            if ((int)uVar18 < 3)
            {
                uVar18 = (uVar18 < 0) ? 0 : uVar18;
            }
            else
            {
                uVar18 = 2;
            }
            iVar2 = 10;
            fVar5 = _graphicUiRemapY2(54.0f);
            fVar4 = _graphicUiRemapX2(100.0f);
            fVar4 = fVar4 * (int)(uVar18 + 1);
            fVar7 = _graphicUiRemapY2(32.0f);
            fVar7 = fVar7 + local_8;
            fVar6 = _graphicUiRemapX2(1110.0f);
            _TODrawMenuPlateXYWHType(fVar6 + local_c, fVar7, fVar4, fVar5, iVar2);
            if (uVar18 == 2)
            {
                fVar10 = 0.11328125f;
                fVar4 = (local_10 + 978.0f) * 0.0009765625f;
                fVar9 = 0.08203125f;
                fVar7 = (local_14 + 834.0f) * 0.0009765625f;
                fVar6 = _graphicUiRemapY2(32.0f);
                fVar11 = _graphicUiRemapX2(local_18 + 144.0f);
                fVar5 = _graphicUiRemapY2(43.0f);
                fVar5 = fVar5 + local_8;
                fVar16 = _graphicUiRemapX2(local_1c + 1152.0f);
                _TOMkpShapeXYWHUV(0x3d, fVar16 + local_c, fVar5, fVar11, fVar6, fVar7, fVar9, fVar4, fVar10);
                fVar10 = 0.6386719f;
                fVar4 = (local_10 + 680.0f) * 0.0009765625f;
                fVar9 = 0.6064453f;
                fVar7 = (local_14 + 614.0f) * 0.0009765625f;
                fVar6 = _graphicUiRemapY2(33.0f);
                fVar11 = _graphicUiRemapX2(local_18 + 66.0f);
                fVar5 = _graphicUiRemapY2(43.0f);
                fVar5 = fVar5 + local_8;
                fVar16 = _graphicUiRemapX2(local_1c + 1265.0f);
                _TOMkpShapeXYWHUV(0x3d, fVar16 + local_c, fVar5, fVar11, fVar6, fVar7, fVar9, fVar4, fVar10);
            }
            p_DAT_01869eea[param_1 * 0xe] = (byte)(p_DAT_01869eea[param_1 * 0xe] + -1);
        }
        _TOMenuDrawKickTmp();
    switchD_008bc339_caseD_0:
        return;
    }

    byte* h_MsWeaponName(int name_id, int owner, int simplified, ushort* ref_model_id)
    {
        int gearid;

        if (owner != 7)
            return _MsWeaponName.orig_fptr(name_id, owner, simplified, ref_model_id);
        gearid = name_id & 0xFFF;
        if (ref_model_id is not null)
        {
            if (gearid >= 74) // Start of Armor IDs
            {
                *ref_model_id = 0x4067; // Seymour Armor
            }
            else
            {
                *ref_model_id = 0x4066; // Seymour Staff
            }
        }
        if (gearid > seymour_gear_names.Length - 1)
            return _MsWeaponName.orig_fptr(name_id, owner, simplified, ref_model_id);

        return (byte*)seymour_gear_names[gearid];
    }

    int h_MsGetItemInternal_00798C20(int param_1, int param_2, int param_3)
    {
        byte bVar1;
        ushort uVar3;
        int iVar4;
        Equipment* gear;
        uint uVar5;
        uint uVar6;
        int iVar7;
        ushort* puVar8;
        int iVar9;
        int iVar10;
        ushort* puVar11;
        int local_14;
        ushort* local_c;
        uint gearOwner;

        iVar4 = _FUN_00798be0((BtlRewardData*)param_3);
        if (iVar4 < 0)
        {
            return -1;
        }
        iVar4 = iVar4 * 0x16;
        gear = (Equipment*)(param_3 + 0xfe + iVar4);
        *(short*)(iVar4 + 0x100 + param_3) = 1;
        iVar7 = 0;
        iVar9 = 0;
        do
        {
            bVar1 = _MsGetSavePlyJoined((byte)iVar7);
            if (bVar1 != 0)
            {
                iVar9 = iVar9 + 1;
            }
            iVar7 = iVar7 + 1;
        } while (iVar7 < 8);
        if (param_1 < 8)
        {
            iVar9 = iVar9 + 3;
        }
        else
        {
            param_1 = 0;
        }
        uVar5 = _Brnd(0xc);
        iVar10 = 0;
        iVar7 = 0;
        do
        {
            if (_MsGetSavePlyJoined((byte)iVar7) != 0)
            {
                iVar10 = iVar10 + 1;
                if ((int)uVar5 % iVar9 < iVar10)
                {
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
        if (gearOwner == 7)
        {
            gearOwner = 3; // Checks if the gear rng rolled belongs to Seymour,
                           // and assigns the gear's auto-abilities to another character's field.
                           // (here, Seymour gets Kimahri's auto-abilities)
        }
        puVar11 = (ushort*)((byte*)param_2 + 0x32 +
                  (*(byte*)(iVar4 + 0x103 + param_3) + gearOwner * 2
                  ) * 0x10);
        local_14 = (int)(iVar9 + (iVar9 >> 0x1f & 7U)) >> 3;
        uVar3 = *puVar11;
        if (((byte)iVar7 == '\0') || (uVar3 == 0))
        {
            iVar4 = 0;
        }
        else
        {
            gear->abilities[0] = uVar3;
            iVar4 = 1;
        }
        if (0 < local_14)
        {
            local_c = &gear->abilities[0] + iVar4;
            do
            {
                if ((int)(uint)gear->slot_count <= iVar4) break;
                uVar5 = _Brnd(0xd);
                uVar3 = puVar11[(int)uVar5 % 7 + 1];
                if (uVar3 != 0)
                {
                    uVar5 = _FUN_00798aa0(uVar3);
                    iVar7 = 0;
                    if (0 < iVar4)
                    {
                        puVar8 = &gear->abilities[0];
                        do
                        {
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
        if (iVar4 < 4)
        {
            puVar11 = &gear->abilities[0] + iVar4;
            for (uVar5 = ((4U - (uint)iVar4) >> 1); uVar5 != 0; uVar5 = uVar5 - 1)
            {
                *(uint*)puVar11 = 0x00FF00FF;
                puVar11 = puVar11 + 2;
            }
            for (uVar5 = uVar5 = (uint)(((4U - iVar4) & 1) != 0 ? 1 : 0); uVar5 != 0; uVar5 = uVar5 - 1)
            {
                *puVar11 = 0xff;
                puVar11 = puVar11 + 1;
            }
        }
        uVar3 = _MsWeaponNameNum(gear);
        gear->name_id = uVar3;
        _MsWeaponName.hook_fptr((int)(uint)uVar3, (int)(uint)gear->owner, 0, &gear->model_id);
        return 0;
    }

    void h_MsChangeWeaponInvisible(uint param_1, byte param_2)
    {
        byte gear;
        Equipment* pSVar2;
        int loop;
        uint chr_id;

        chr_id = param_1 & 0xff;
        if (chr_id < 8)
        {
            loop = 0;
            do
            {
                if (loop == 0)
                {
                    gear = Globals.save_data->ply_saves[(int)chr_id].wpn_inv_idx;
                }
                else
                {
                    gear = Globals.save_data->ply_saves[(int)chr_id].arm_inv_idx;
                }
                if (gear != 0xff)
                {
                    pSVar2 = _MsGetSaveWeapon(gear, (byte**)0x0);
                    pSVar2->flags = (byte)(pSVar2->flags ^ (param_2 * '\x02' ^ pSVar2->flags) & 2);
                }
                loop = loop + 1;
            } while (loop < 2);
        }
        return;
    }

    // Gear Ability preview in shops
    void h_FUN_008d85f0(int param_1, int param_2)
    {
        void* pvVar1;
        Equipment* pSVar2;
        uint gear_inv_idx;
        Equipment* pSVar3;
        byte* pbVar4;
        float fVar5;
        float fVar6;
        float fVar7;
        float fVar8;
        float* pfVar9;
        Equipment* pSVar10;
        int uVar11;
        int scale;
        int iVar12;
        byte* local_14;
        byte* local_10;
        float local_c;
        float local_8;

        if (7 < DAT_0186ab60)
        {
            return;
        }
        pvVar1 = _TkMn2GetExcelData(*p_DAT_0186aadc_curShopIdx, (ExcelDataFile*)*(nint*)p_DAT_0186ab68_arms_shop_bin_ptr);
        if (param_2 == 0)
        {
            pSVar2 = _MsGetSaveWeapon(p_DAT_01597730_OvrModesMenuList[*(short*)(param_1 + 0x48)].
                                           overdrive_id, &local_10);
        }
        else
        {
            pSVar2 = (Equipment*)_FUN_008d9140(*(ushort*)((int)pvVar1 + *(short*)(param_1 + 0x48) * 2 + 2));
        }
        if (pSVar2->type == 0)
        {
            gear_inv_idx = _FUN_008a9c20(DAT_0186ab60);
        }
        else
        {
            gear_inv_idx = _FUN_008a97d0(DAT_0186ab60);
        }
        if (gear_inv_idx == 0xff)
        {
            pSVar3 = (Equipment*)0x0;
        }
        else
        {
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
        if (param_2 == 0)
        {
            if (pSVar3 == (Equipment*)0x0)
            {
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
        else if (pSVar3 == (Equipment*)0x0)
        {
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
            goto LAB_008d8976;
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
    LAB_008d8976:
        pbVar4 = (byte*)_FUN_008bee40(0x17);
        _ToMakeBtlEasyFont(pbVar4, fVar5, fVar6, 0.0f, fVar7);
        goto LAB_008d898c;
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
        return;
    }

    int h_MsLimitTypeDamageCheck(uint param_1, int param_2, uint param_3, int param_4, int param_5, int param_6, int param_7)
    {
        uint uVar1;
        uint uVar2;
        Chr* character;
        int iVar3;
        int iVar4;
        int iVar5;

        iVar5 = param_5;
        iVar4 = 0;
        uVar1 = _MsGetRamChrMonster(param_1);
        uVar2 = _MsGetRamChrMonster(param_3);
        if ((((*(byte*)(param_4 + 0x5bb) == '\x13') && (-1 < param_5)) && (uVar1 == 1)) && (uVar2 == 0))
        {
            _MsLimitUp((int)param_3, (Chr*)param_4, (uint)(param_6 * 0x12) / *(uint*)(param_4 + 0x594) + 1);
        }
        if (param_5 < 1)
        {
            if (((param_5 < 0) && (uVar1 == 0)) && ((uVar2 == 0 && (param_1 != param_3))))
            {
                iVar4 = 1;
                _FUN_007b10d0.hook_fptr(param_1, 3, 0);
                if (*(byte*)(param_2 + 0x5bb) == '\x03')
                {
                    iVar3 = (int)(*(uint*)(param_4 + 0x594) - *(int*)(param_4 + 0x5d0));
                    iVar5 = -param_5;
                    if (-iVar3 != param_5 && iVar3 <= -param_5)
                    {
                        iVar5 = iVar3;
                    }
                    _MsLimitUp((int)param_1, (Chr*)param_2, (uint)(iVar5 << 4) / *(uint*)(param_4 + 0x594) + 1);
                }
            }
        }
        else if (uVar1 == 1)
        {
            if (uVar2 == 0)
            {
                _FUN_007b10d0.hook_fptr(param_3, 2, 0);
                if (*(byte*)(param_4 + 0x5bb) == '\x02')
                {
                    _MsLimitUp((int)param_3, (Chr*)param_4, (uint)(param_5 * 0x1e) / *(uint*)(param_4 + 0x594) + 1);
                }
                uVar1 = 0;
                param_5 = 1;
                do
                {
                    character = _MsGetChr(uVar1);
                    if ((character->in_battle != 0) && (uVar1 != param_3))
                    {
                        param_5 = param_5 + 1;
                        _FUN_007b10d0.hook_fptr(uVar1, 1, 0);
                        if (character->ram.limit_mode_selected == 1)
                        {
                            _MsLimitUp((int)uVar1, character, (uint)(iVar5 * 0x14) / *(uint*)(param_4 + 0x594) + 1);
                        }
                    }
                    uVar1 = uVar1 + 1;
                } while ((int)uVar1 < 8);
                return param_5;
            }
        }
        else if (((uVar1 == 0) && (uVar2 == 1)) && (param_7 != 0))
        {
            iVar4 = 1;
            _FUN_007b10d0.hook_fptr(param_1, 0, 0);
            if (*(byte*)(param_2 + 0x5bb) == '\0')
            {
                uVar1 = (uint)((param_5 * 10) / *(int*)(param_2 + 0x6f4) + 1); //
                if (0x10 < (int)uVar1)
                {
                    uVar1 = 0x10;
                }
                _MsLimitUp((int)param_1, (Chr*)param_2, uVar1);
            }
            if (*(byte*)(param_2 + 0x5bb) == '\x13')
            {
                _MsLimitUp((int)param_1, (Chr*)param_2, (uint)(((param_5 << 4) / *(int*)(param_2 + 0x6f4)) / 10 + 1));
                return 1;
            }
        }
        return iVar4;
    }

    int h_MsLimitTypeDeathCheck(int param_1, int param_2, uint param_3, int param_4)
    {
        int uVar1;
        uint uVar2;
        Chr* character;
        int iVar3;

        iVar3 = 0;
        uVar1 = (int)_MsGetRamChrMonster((uint)param_1);
        uVar2 = _MsGetRamChrMonster(param_3);
        if (uVar1 == 1)
        {
            if (uVar2 == 0)
            {
                uVar1 = 0;
                do
                {
                    character = _MsGetChr((uint)uVar1);
                    if ((character->in_battle != 0) && (uVar1 != param_3))
                    {
                        iVar3 = iVar3 + 1;
                        _FUN_007b10d0.hook_fptr((uint)uVar1, 7, 0);
                        if (character->ram.limit_mode_selected == 7)
                        {
                            _MsLimitUp(uVar1, character, 0x1e);
                        }
                    }
                    uVar1 = uVar1 + 1;
                } while (uVar1 < 8);
                return iVar3;
            }
        }
        else if ((uVar1 == 0) && (uVar2 == 1))
        {
            _FUN_007b10d0.hook_fptr((uint)param_1, 8, 0);
            if (*(byte*)(param_2 + 0x5bb) == '\b')
            {
                _MsLimitUp(param_1, (Chr*)param_2, 0x14);
            }
            if ((*(int*)(param_2 + 0x6f4) * 0x14 < *(int*)(param_4 + 0x594)) ||
               (9999 < *(int*)(param_4 + 0x594)))
            {
                _FUN_007b10d0.hook_fptr((uint)param_1, 9, 0);
            }
            if ((*(byte*)(param_2 + 0x5bb) == '\t') &&
               ((uint)(*(int*)(param_2 + 0x6f4) * 3) < *(uint*)(param_4 + 0x594)))
            {
                _MsLimitUp(param_1, (Chr*)param_2, 0x14);
            }
        }
        return 0;
    }

    int h_FUN_007b10d0(uint chr_id, uint limit_mode, int param_3)
    {
        Chr* chr = _MsGetChr(chr_id);

        if (Globals.Battle.btl->battle_type == 0 && chr_id < 8 && limit_mode < 0x11 && chr->stat_death == 0 && chr->stat_stone == 0 || param_3 != 0)
        {
            int mask = 1 << ((byte)limit_mode & 0x1F);
            if ((*(int*)((byte*)chr + 0x6F0) & mask) == 0)
            {
                PlySave* ply_save = &Globals.save_data->ply_saves[(int)chr_id];
                if (ply_save->limit_mode_counters[(int)limit_mode] != 0xFFFF)
                {
                    *(int*)((byte*)chr + 0x6F0) |= mask;
                    if (ply_save->limit_mode_counters[(int)limit_mode] != 0)
                    {
                        ply_save->limit_mode_counters[(int)limit_mode] -= 1;
                    }
                    if (ply_save->limit_mode_counters[(int)limit_mode] == 0 && !ply_save->obtained_limit_modes.HasFlag((OverdriveModeFlags)limit_mode))
                    {
                        *(byte*)((nint)Globals.Battle.btl + 0x175B) = 1;
                        return 1;
                    }
                }
            }
        }
        return 0;
    }

    int h_MsLimitTypeTurnCheck(uint param_1, int param_2)
    {
        uint uVar1;
        int iVar2;
        uint uVar3;
        Chr* character;
        uint chr_id;
        int local_8;

        uVar1 = param_1;
        if (7 < param_1)
        {
            return 0;
        }
        local_8 = 1;
        _FUN_007b10d0.hook_fptr(param_1, 0xd, 0);
        if (*(byte*)(param_2 + 0x5bb) == '\r')
        {
            _MsLimitUp((int)param_1, (Chr*)param_2, 3);
        }
        iVar2 = _MsCalcWeakLevel(*(int*)(param_2 + 0x5d0), *(int*)(param_2 + 0x594));
        if (0 < iVar2)
        {
            local_8 = 2;
            _FUN_007b10d0.hook_fptr(param_1, 0xf, 0);
            if (*(byte*)(param_2 + 0x5bb) == '\x0f')
            {
                _MsLimitUp((int)param_1, (Chr*)param_2, 5);
            }
        }
        uVar3 = 0;
        param_1 = 0;
        chr_id = 0;
        do
        {
            character = _MsGetChr(chr_id);
            uVar3 = param_1;
            if ((chr_id != uVar1) && (character->in_battle != 0) && (character->stat_action != 0) && (character->stat_death == 0) && (character->stat_stone == 0))
            {
                param_1 = param_1 + 1;
                uVar3 = param_1;
            }
            chr_id = chr_id + 1;
        } while ((int)chr_id < 0x12);
        if (uVar3 == 0)
        {
            local_8 = local_8 + 1;
            _FUN_007b10d0.hook_fptr(uVar1, 0x10, 0);
            if (*(byte*)(param_2 + 0x5bb) == '\x10')
            {
                _MsLimitUp((int)uVar1, (Chr*)param_2, 0x10);
            }
        }
        if (((((*(ushort*)(param_2 + 0x606) & 10) == 0) && ((*(ushort*)(param_2 + 0x606) & 0x100) == 0))
            && ((*(byte*)(param_2 + 0x608) == '\0' &&
                (((*(byte*)(param_2 + 0x609) == '\0' && (*(byte*)(param_2 + 0x60a) == '\0')) &&
                 (*(byte*)(param_2 + 0x614) == '\0')))))) &&
           ((*(ushort*)(param_2 + 0x616) & 0x4000) == 0))
        {
            return local_8;
        }
        _FUN_007b10d0.hook_fptr(uVar1, 0xe, 0);
        if (*(byte*)(param_2 + 0x5bb) == '\x0e')
        {
            _MsLimitUp((int)uVar1, (Chr*)param_2, 0x10);
        }
        return local_8 + 1;
    }

    int h_MsLimitTypeWinCheck()
    {
        Chr* character;
        int iVar1;
        int chr_id;

        iVar1 = 0;
        chr_id = 0;
        do
        {
            character = _MsGetChr((uint)chr_id);
            if (character->in_battle != 0)
            {
                iVar1 = iVar1 + 1;
                _FUN_007b10d0.hook_fptr((uint)chr_id, 0xb, 0);
                if (character->ram.limit_mode_selected == 0xb)
                {
                    _MsLimitUp(chr_id, character, 0x14);
                }
            }
            chr_id = chr_id + 1;
        } while (chr_id < 8);
        return iVar1;
    }

    void h_MsSetSaveStartGame()
    {
        _MsSetSaveStartGame.orig_fptr();

        Globals.save_data->ability_map_limit.has_extra_24 = true;

        for (int i = 0; i < 200; i++)
        {
            Equipment* gear = &Globals.save_data->equipment[i];
            if (gear->exists && gear->owner == 7)
            {
                gear->flags = 2;
                if (gear->type == 1)
                {
                    gear->slot_count = 1;
                    gear->abilities[0] = 0xFF;
                    gear->name_id = _MsWeaponNameNum(gear);
                }
                else
                {
                    gear->slot_count = 2;
                    gear->abilities[1] = 0x8000;
                    gear->name_id = _MsWeaponNameNum(gear);
                }
                _MsWeaponName.hook_fptr(gear->name_id, gear->owner, 0, &gear->model_id);
            }
        }

        PlySave* seymour = _MsGetSavePlayerPtr(7);
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
        requiem->is_piercing = true;
        requiem->flags_damage = 4; // Can Crit
        requiem->dmg_formula = 15; // Special MAG

        Command* extra24 = _MsGetRomPlyCommand(0x3130, (int*)0x0);
        extra24->name_offset = 12487; // "Summon"
        extra24->desc_offset = 12496; // "Summon an aeon."
        extra24->icon = 19;
        extra24->is_top_level_in_menu = true;
        extra24->opens_sub_menu = true;
        extra24->sub_menu_cat2 = 5;
        extra24->sub_menu_cat = 5;
        extra24->user_id = 7;
        extra24->flags_target = 0;
        extra24->display_move_name = false;
        extra24->is_in_trigger_menu = true;
        extra24->show_user_casting_effects = true;
        extra24->limit_cost = 100;
    }

    // Show Seymour's Armor model
    int h_FUN_00635c20(uint param_1) 
    {
        if (param_1 == 0x4067)
        {
            param_1 = 0x4068;
        }
        return _FUN_00635c20.orig_fptr(param_1);
    }

    // Extra24 = Seymour Summon
    int h_MsParseCommand(byte* param_1)
    {
        uint uVar13 = param_1[2];
        int iVar14 = (int)uVar13 * 0x10;
        ushort* com_id = (ushort*)(param_1 + iVar14 + 8);

        if (*com_id == 0x3130)
        {
            *com_id = 0x3117;
            int result = _MsParseCommand.orig_fptr(param_1);
            *com_id = 0x3130;
            return result;
        }
        return _MsParseCommand.orig_fptr(param_1);
    }

    // Extra24 Summon Help text
    void h_TOBtlCtrlHelpWin(int param_1)
    {
        int win_idx = *toBwNum;
        BtlWindow* currentwindow = &Globals.Battle.windows[win_idx];

        if (currentwindow->window_command_id == 0x3130)
        {
            currentwindow->window_command_id = 0x3117;
            _TOBtlCtrlHelpWin.orig_fptr(param_1);
            currentwindow->window_command_id = 0x3130;
            return;
        }
        _TOBtlCtrlHelpWin.orig_fptr(param_1);
    }

    // Battle Summon List
    ushort* h_TOGetSaveWindow(uint chr_id, BtlWindowType window_type, int* summonlistlength)
    {
        if ((uint)window_type == 5)
        {
            ushort* originallist = _TOGetSaveWindow.orig_fptr(chr_id, window_type, summonlistlength);
            int originalcount = *summonlistlength;
            int newcount = 0;

            for (int i = 0; i < originalcount; i++)
            {
                ushort aeonId = originallist[i];
                if (aeonId == 0xFFFF)
                    break;
                if (chr_id == 7)
                {
                    if (aeonId == 0x000D)
                    {
                        _filteredSummonList[newcount++] = aeonId;
                    }
                }
                else if (chr_id != 7)
                {
                    if (aeonId != 0x000D || Globals.save_data->has_anima) // Can't summon Anima until Baaj Sidequest
                    {
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

    // Overdrive Menus
    int h_TkMenuSummonEnableMask()
    {
        uint cur_chr_id = _TkMenuGetCurrentPlayer();
        uint original = (uint)_TkMenuSummonEnableMask.orig_fptr();

        if (cur_chr_id == 1)
        {
            if (!Globals.save_data->has_anima)
            {
                return (int)(original & ~(1u << 0x0D)); // Display Anima in Yuna's menu once unlocked
            }
        }
        return (int)original;
    }
}
