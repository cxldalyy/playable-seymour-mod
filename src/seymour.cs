// SPDX-License-Identifier: MIT

namespace Fahrenheit.Mods.Seymour;

[FhLoad(FhGameId.FFX)]
public unsafe class SeymourModule : FhModule {
    const string game = "FFX.exe";
    public SeymourModule() { }
    static SeymourModule()
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

        string scene0 = "scene0";
        ReadOnlySpan<byte> scene0Utf8 = Encoding.UTF8.GetBytes(scene0);
        scene0String = (byte*)NativeMemory.AllocZeroed((nuint)scene0Utf8.Length + 1);
        scene0Utf8.CopyTo(new Span<byte>(scene0String, scene0Utf8.Length));

        string scene11 = "scene11";
        ReadOnlySpan<byte> scene11Utf8 = Encoding.UTF8.GetBytes(scene11);
        scene11String = (byte*)NativeMemory.AllocZeroed((nuint)scene11Utf8.Length + 1);
        scene11Utf8.CopyTo(new Span<byte>(scene11String, scene11Utf8.Length));

        string scene20 = "scene20";
        ReadOnlySpan<byte> scene20Utf8 = Encoding.UTF8.GetBytes(scene20);
        scene20String = (byte*)NativeMemory.AllocZeroed((nuint)scene20Utf8.Length + 1);
        scene20Utf8.CopyTo(new Span<byte>(scene20String, scene20Utf8.Length));
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int AtelPopStackInteger(AtelBasicWorker* work, AtelStack* stack);
    private static FhMethodHandle<AtelPopStackInteger> _AtelPopStackInteger =>
        new ( new FhMethodLocation("FFX.exe", 0x46DE90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate PlySave* MsGetSavePlayerPtr(uint chr_id);
    private static FhMethodHandle<MsGetSavePlayerPtr> _MsGetSavePlayerPtr =>
        new ( new FhMethodLocation("FFX.exe", 0x3853F0) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate uint TkMenuGetPlayerListMax2();
    private static FhMethodHandle<TkMenuGetPlayerListMax2> _TkMenuGetPlayerListMax2 =>
        new ( new FhMethodLocation("FFX.exe", 0x4A9B00) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void TkVU1SyncPath();
    private static FhMethodHandle<TkVU1SyncPath> _TkVU1SyncPath =>
        new ( new FhMethodLocation("FFX.exe", 0x48EBD0) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void TOMenuOpenPktBuffTmp();
    private static FhMethodHandle<TOMenuOpenPktBuffTmp> _TOMenuOpenPktBuffTmp =>
        new ( new FhMethodLocation("FFX.exe", 0x4BEF00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float graphicUiRemapX2(float x);
    private static FhMethodHandle<graphicUiRemapX2> _graphicUiRemapX2 =>
        new ( new FhMethodLocation("FFX.exe", 0x244990) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void TODrawMenuBG();
    private static FhMethodHandle<TODrawMenuBG> _TODrawMenuBG =>
        new ( new FhMethodLocation("FFX.exe", 0x4F5C10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_0088e6c0(int param_1);
    private static FhMethodHandle<FUN_0088e6c0> _FUN_0088e6c0 =>
        new ( new FhMethodLocation("FFX.exe", 0x48E6C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte TkMenuGetPlayerFromIndex2(int param_1);
    private static FhMethodHandle<TkMenuGetPlayerFromIndex2> _TkMenuGetPlayerFromIndex2 =>
        new ( new FhMethodLocation("FFX.exe", 0x4A9AB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float graphicUiRemapY2(float y);
    private static FhMethodHandle<graphicUiRemapY2> _graphicUiRemapY2 =>
        new ( new FhMethodLocation("FFX.exe", 0x2449D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMkpShapeXYWHUV(int param_1, float x, float y, float w, float h, float uv_x1, float uv_y1, float uv_x2, float uv_y2);
    private static FhMethodHandle<TOMkpShapeXYWHUV> _TOMkpShapeXYWHUV =>
        new ( new FhMethodLocation("FFX.exe", 0x503BB0) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate uint FUN_008a9b20();
    private static FhMethodHandle<FUN_008a9b20> _FUN_008a9b20 =>
        new ( new FhMethodLocation("FFX.exe", 0x4A9B20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008e7d30(float param_1, float param_2, float param_3, float param_4, float param_5, float param_6, float param_7, float param_8, float param_9, float param_10, uint param_11, uint param_12);
    private static FhMethodHandle<FUN_008e7d30> _FUN_008e7d30 =>
        new ( new FhMethodLocation("FFX.exe", 0x4E7D30) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte FUN_008a9a20(uint param_1);
    private static FhMethodHandle<FUN_008a9a20> _FUN_008a9a20 =>
        new ( new FhMethodLocation("FFX.exe", 0x4A9A20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* TOGetSaveChrName(uint chr_id);
    private static FhMethodHandle<TOGetSaveChrName> _TOGetSaveChrName =>
        new ( new FhMethodLocation("FFX.exe", 0x4AC800) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_00905930(byte* name, float param_2, float param_3, byte color, float param_5, int param_6);
    private static FhMethodHandle<FUN_00905930> _FUN_00905930 =>
        new ( new FhMethodLocation("FFX.exe", 0x505930) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate FhLangId TOGetFFXLang();
    private static FhMethodHandle<TOGetFFXLang> _TOGetFFXLang =>
        new ( new FhMethodLocation("FFX.exe", 0x4AC2A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint FUN_008a9940(uint param_1);
    private static FhMethodHandle<FUN_008a9940> _FUN_008a9940 =>
        new ( new FhMethodLocation("FFX.exe", 0x4A9940) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint FUN_008a9870(uint param_1);
    private static FhMethodHandle<FUN_008a9870> _FUN_008a9870 =>
        new ( new FhMethodLocation("FFX.exe", 0x4A9870) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_00901660(byte* param_1, float param_2, float param_3, byte param_4, float param_5, float param_6);
    private static FhMethodHandle<FUN_00901660> _FUN_00901660 =>
        new ( new FhMethodLocation("FFX.exe", 0x501660) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint FUN_008a9960(uint param_1);
    private static FhMethodHandle<FUN_008a9960> _FUN_008a9960 =>
        new ( new FhMethodLocation("FFX.exe", 0x4A9960) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint FUN_008a9920(uint param_1);
    private static FhMethodHandle<FUN_008a9920> _FUN_008a9920 =>
        new ( new FhMethodLocation("FFX.exe", 0x4A9920) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte FUN_008a9b30(byte param_1);
    private static FhMethodHandle<FUN_008a9b30> _FUN_008a9b30 =>
        new ( new FhMethodLocation("FFX.exe", 0x4A9B30) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_00905230(int param_1, float* param_2, float param_3, float param_4);
    private static FhMethodHandle<FUN_00905230> _FUN_00905230 =>
        new ( new FhMethodLocation("FFX.exe", 0x505230) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_00905820(int param_1, float param_2, float param_3, byte param_4, float param_5, float param_6);
    private static FhMethodHandle<FUN_00905820> _FUN_00905820 =>
        new ( new FhMethodLocation("FFX.exe", 0x505820) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TkMn2DrawCrossCursor(float x, float y, float param_3);
    private static FhMethodHandle<TkMn2DrawCrossCursor> _TkMn2DrawCrossCursor =>
        new ( new FhMethodLocation("FFX.exe", 0x4C0640) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate byte FUN_008a9c10();
    private static FhMethodHandle<FUN_008a9c10> _FUN_008a9c10 =>
        new ( new FhMethodLocation("FFX.exe", 0x4A9C10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008c13b0(float x, float y, int param_3);
    private static FhMethodHandle<FUN_008c13b0> _FUN_008c13b0 =>
        new ( new FhMethodLocation("FFX.exe", 0x4C13B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TODrawCrossBoxXYWHC2(float x, float y, float w, float h, uint color_start, uint color_end);
    private static FhMethodHandle<TODrawCrossBoxXYWHC2> _TODrawCrossBoxXYWHC2 =>
        new ( new FhMethodLocation("FFX.exe", 0x4F4B20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsGetGIL(AtelBasicWorker* work, int* storage, AtelStack* stack);
    private static FhMethodHandle<MsGetGIL> _MsGetGIL =>
        new ( new FhMethodLocation("FFX.exe", 0x384F40) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008c09f0(float param_1, float param_2, float param_3, float param_4, int param_5);
    private static FhMethodHandle<FUN_008c09f0> _FUN_008c09f0 =>
        new ( new FhMethodLocation("FFX.exe", 0x4C09F0) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int FUN_008a9c00();
    private static FhMethodHandle<FUN_008a9c00> _FUN_008a9c00 =>
        new ( new FhMethodLocation("FFX.exe", 0x4A9C00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008e19f0(uint param_1, float param_2, float param_3, byte param_4, int param_5);
    private static FhMethodHandle<FUN_008e19f0> _FUN_008e19f0 =>
        new ( new FhMethodLocation("FFX.exe", 0x4E19F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMkpShapeXYWHUVC2(uint param_1, float x, float y, float w, float h, float param_6, float param_7, float param_8, float param_9, RGBA8 color_1, RGBA8 color_2);
    private static FhMethodHandle<TOMkpShapeXYWHUVC2> _TOMkpShapeXYWHUVC2 =>
        new ( new FhMethodLocation("FFX.exe", 0x503EE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int AtelGetSaveDic(AtelBasicWorker* work, int* storage, AtelStack* stack);
    private static FhMethodHandle<AtelGetSaveDic> _AtelGetSaveDic =>
        new ( new FhMethodLocation("FFX.exe", 0x46C3A0) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int MsGetSaveConfigEnglish();
    private static FhMethodHandle<MsGetSaveConfigEnglish> _MsGetSaveConfigEnglish =>
        new ( new FhMethodLocation("FFX.exe", 0x385290) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int AtelGetSaveDicName(uint param_1, int param_2);
    private static FhMethodHandle<AtelGetSaveDicName> _AtelGetSaveDicName =>
        new ( new FhMethodLocation("FFX.exe", 0x46C3C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TkMenuDraw1612Width(byte* param_1);
    private static FhMethodHandle<TkMenuDraw1612Width> _TkMenuDraw1612Width =>
        new ( new FhMethodLocation("FFX.exe", 0x4DC9C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMkpCrossExtMesFontLClut(int param_1, byte* text, float x, float y, byte color, float scale, float p7_unused);
    private static FhMethodHandle<TOMkpCrossExtMesFontLClut> _TOMkpCrossExtMesFontLClut =>
        new ( new FhMethodLocation("FFX.exe", 0x5016B0) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void TOMenuDrawKickTmp();
    private static FhMethodHandle<TOMenuDrawKickTmp> _TOMenuDrawKickTmp =>
        new ( new FhMethodLocation("FFX.exe", 0x4BE9F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* TOGetShapTextureName(int param_1);
    private static FhMethodHandle<TOGetShapTextureName> _TOGetShapTextureName =>
        new ( new FhMethodLocation("FFX.exe", 0x4AC870) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOGetImageWH(int param_1, float* w, float* h);
    private static FhMethodHandle<TOGetImageWH> _TOGetImageWH =>
        new ( new FhMethodLocation("FFX.exe", 0x4AC3B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void graphicDrawUIElement(graphicDrawUIAbmapElement_param1* param_1, byte* param_2, int param_3, int param_4, int param_5);
    private static FhMethodHandle<graphicDrawUIElement> _graphicDrawUIElement =>
        new ( new FhMethodLocation("FFX.exe", 0x23F090) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate byte AtelGetAlbhedRikku();
    private static FhMethodHandle<AtelGetAlbhedRikku> _AtelGetAlbhedRikku =>
        new ( new FhMethodLocation("FFX.exe", 0x46A770) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate SaveData* MsGetSaveEventAddress();
    private static FhMethodHandle<MsGetSaveEventAddress> _AtelGetEventSaveRamAdrs =>
        new ( new FhMethodLocation("FFX.exe", 0x385300) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MsGetSavePartyMember(uint* ref_frontline_0, uint* ref_frontline_1, uint* ref_frontline_2);
    private static FhMethodHandle<MsGetSavePartyMember> _MsGetSavePartyMember =>
        new ( new FhMethodLocation("FFX.exe", 0x3853B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool MsGetSavePlyJoin(uint chr_id);
    private static FhMethodHandle<MsGetSavePlyJoin> _MsGetSavePlyJoin =>
        new ( new FhMethodLocation("FFX.exe", 0x385440) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MsSetSavePlyJoin(uint _chr_id, int enable);
    private static FhMethodHandle<MsSetSavePlyJoin> _MsSetSavePlyJoin =>
        new ( new FhMethodLocation("FFX.exe", 0x386A70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_00786a10(uint param_1, uint param_2, uint param_3);
    private static FhMethodHandle<FUN_00786a10> _FUN_00786a10 =>
        new ( new FhMethodLocation("FFX.exe", 0x386A10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_0088e6a0(int param_1);
    private static FhMethodHandle<FUN_0088e6a0> _FUN_0088e6a0 =>
        new ( new FhMethodLocation("FFX.exe", 0x48E6A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint FUN_008bd9d0(int param_1);
    private static FhMethodHandle<FUN_008bd9d0> _FUN_008bd9d0 =>
        new ( new FhMethodLocation("FFX.exe", 0x4BD9D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_009055c0(int param_1, float param_2, float param_3, int param_4, float param_5, float param_6);
    private static FhMethodHandle<FUN_009055c0> _FUN_009055c0 =>
        new ( new FhMethodLocation("FFX.exe", 0x5055C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_00905550(int param_1, float param_2, float param_3, byte param_4, float param_5);
    private static FhMethodHandle<FUN_00905550> _FUN_00905550 =>
        new ( new FhMethodLocation("FFX.exe", 0x505550) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte FUN_008bda10(byte param_1);
    private static FhMethodHandle<FUN_008bda10> _FUN_008bda10 =>
        new ( new FhMethodLocation("FFX.exe", 0x4BDA10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsGetNextAP(int chr_id);
    private static FhMethodHandle<MsGetNextAP> _MsGetNextAP =>
        new ( new FhMethodLocation("FFX.exe", 0x384F50) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_00785370(byte param_1);
    private static FhMethodHandle<FUN_00785370> _FUN_00785370 =>
        new ( new FhMethodLocation("FFX.exe", 0x385370) );


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TODrawMenuPlateXYWHType(float x, float y, float w, float h, int type);
    private static FhMethodHandle<TODrawMenuPlateXYWHType> _TODrawMenuPlateXYWHType =>
        new ( new FhMethodLocation("FFX.exe", 0x4F5F70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* MsGetSaveWeaponName(uint inv_idx);
    private static FhMethodHandle<MsGetSaveWeaponName> _MsGetSaveWeaponName =>
        new ( new FhMethodLocation("FFX.exe", 0x3ABE10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DrawCrossMenuIconXYWHRGBA(float param_1, float param_2, float param_3, float param_4, byte param_5, byte param_6, byte param_7, byte param_8, byte param_9);
    private static FhMethodHandle<DrawCrossMenuIconXYWHRGBA> _DrawCrossMenuIconXYWHRGBA =>
        new ( new FhMethodLocation("FFX.exe", 0x4E6AF0) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate uint MsGetSaveConfigHiragana();
    private static FhMethodHandle<MsGetSaveConfigHiragana> _MsGetSaveConfigHiragana =>
        new ( new FhMethodLocation("FFX.exe", 0x3852B0) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate double graphicGetTime();
    private static FhMethodHandle<graphicGetTime> _graphicGetTime =>
        new ( new FhMethodLocation("FFX.exe", 0x2415C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008e6cc0(float param_1, float param_2, float param_3, float param_4, int param_5, int param_6, int param_7);
    private static FhMethodHandle<FUN_008e6cc0> _FUN_008e6cc0 =>
        new ( new FhMethodLocation("FFX.exe", 0x4E6CC0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMakePktScissor(int param_1, int param_2, int param_3, int param_4);
    private static FhMethodHandle<TOMakePktScissor> _TOMakePktScissor =>
        new ( new FhMethodLocation("FFX.exe", 0x4FDEE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint TOCheckBtlCommandUse(uint param_1, uint param_2);
    private static FhMethodHandle<TOCheckBtlCommandUse> _TOCheckBtlCommandUse =>
        new ( new FhMethodLocation("FFX.exe", 0x49AC10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Command* MsGetComData(uint com_id, int* out_name);
    private static FhMethodHandle<MsGetComData> _MsGetComData =>
        new ( new FhMethodLocation("FFX.exe", 0x39A4C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsGetSaveItemNum(uint param_1);
    private static FhMethodHandle<MsGetSaveItemNum> _MsGetSaveItemNum =>
        new ( new FhMethodLocation("FFX.exe", 0x390500) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsGetCommandMP(uint chr_id, uint command);
    private static FhMethodHandle<MsGetCommandMP> _MsGetCommandMP =>
        new ( new FhMethodLocation("FFX.exe", 0x38D030) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsGetRamChrHP(uint chr_id);
    private static FhMethodHandle<MsGetRamChrHP> _MsGetRamChrHP =>
        new ( new FhMethodLocation("FFX.exe", 0x39ADE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsGetRamChrMP(uint chr_id);
    private static FhMethodHandle<MsGetRamChrMP> _MsGetRamChrMP =>
        new ( new FhMethodLocation("FFX.exe", 0x39AE60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_00904ba0(byte* param_1, float param_2, float param_3, float param_4, byte param_5, float param_6, uint param_7, int param_8, int param_9, int param_10);
    private static FhMethodHandle<FUN_00904ba0> _FUN_00904ba0 =>
        new ( new FhMethodLocation("FFX.exe", 0x504BA0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void updateMenu(IntPtr menu);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate uint FUN_008a9820();
    private static FhMethodHandle<FUN_008a9820> _FUN_008a9820 =>
        new ( new FhMethodLocation("FFX.exe", 0x4A9820) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate bool FUN_008cfc00();
    private static FhMethodHandle<FUN_008cfc00> _FUN_008cfc00 =>
        new ( new FhMethodLocation("FFX.exe", 0x4CFC00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008cfcf0(int param_1, int param_2);
    private static FhMethodHandle<FUN_008cfcf0> _FUN_008cfcf0 =>
        new ( new FhMethodLocation("FFX.exe", 0x4CFCF0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008c2bd0(byte* param_1);
    private static FhMethodHandle<FUN_008c2bd0> _FUN_008c2bd0 =>
        new ( new FhMethodLocation("FFX.exe", 0x4C2BD0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_00798be0(BtlRewardData* get_data);
    private static FhMethodHandle<FUN_00798be0> _FUN_00798be0 =>
        new ( new FhMethodLocation("FFX.exe", 0x398BE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte MsGetSavePlyJoined(byte idx);
    private static FhMethodHandle<MsGetSavePlyJoined> _MsGetSavePlyJoined =>
        new ( new FhMethodLocation("FFX.exe", 0x385460) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint Brnd(int param_1);
    private static FhMethodHandle<Brnd> _Brnd =>
        new ( new FhMethodLocation("FFX.exe", 0x398900) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int MsCheckRange(int n, int min, int max);
    private static FhMethodHandle<MsCheckRange> _MsCheckRange =>
        new ( new FhMethodLocation("FFX.exe", 0x39A0D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint FUN_00798aa0(uint param_1);
    private static FhMethodHandle<FUN_00798aa0> _FUN_00798aa0 =>
        new ( new FhMethodLocation("FFX.exe", 0x398AA0) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate byte MsWeaponNameNum(Equipment* gear);
    private static FhMethodHandle<MsWeaponNameNum> _MsWeaponNameNum =>
        new ( new FhMethodLocation("FFX.exe", 0x3A0D10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Equipment* MsGetSaveWeapon(uint gear_inv_idx, byte** ref_name);
    private static FhMethodHandle<MsGetSaveWeapon> _MsGetSaveWeapon =>
        new ( new FhMethodLocation("FFX.exe", 0x3ABBF0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void* TkMn2GetExcelData(int req_elem_idx, ExcelDataFile* excel_data_ptr);
    private static FhMethodHandle<TkMn2GetExcelData> _TkMn2GetExcelData =>
        new ( new FhMethodLocation("FFX.exe", 0x4C1AD0) );

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
    private static FhMethodHandle<FUN_008d9140> _FUN_008d9140 =>
        new ( new FhMethodLocation("FFX.exe", 0x4D9140) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte FUN_008a9c20(uint param_1);
    private static FhMethodHandle<FUN_008a9c20> _FUN_008a9c20 =>
        new ( new FhMethodLocation("FFX.exe", 0x4A9C20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte FUN_008a97d0(uint param_1);
    private static FhMethodHandle<FUN_008a97d0> _FUN_008a97d0 =>
        new ( new FhMethodLocation("FFX.exe", 0x4A97D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008f8bb0(int param_1, float param_2, float param_3, float param_4, float param_5);
    private static FhMethodHandle<FUN_008f8bb0> _FUN_008f8bb0 =>
        new ( new FhMethodLocation("FFX.exe", 0x4F8BB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void* FUN_008bee40(uint param_1);
    private static FhMethodHandle<FUN_008bee40> _FUN_008bee40 =>
        new ( new FhMethodLocation("FFX.exe", 0x4BEE40) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ToGetBtlEasyFontWidth(byte* text, float* ref_width, int param_3, float scale);
    private static FhMethodHandle<ToGetBtlEasyFontWidth> _ToGetBtlEasyFontWidth =>
        new ( new FhMethodLocation("FFX.exe", 0x505290) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008d8a70(float param_1, float param_2, int param_3);
    private static FhMethodHandle<FUN_008d8a70> _FUN_008d8a70 =>
        new ( new FhMethodLocation("FFX.exe", 0x4D8A70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ToMakeBtlEasyFont(byte* param_1, float param_2, float param_3, float param_4, float param_5);
    private static FhMethodHandle<ToMakeBtlEasyFont> _ToMakeBtlEasyFont =>
        new ( new FhMethodLocation("FFX.exe", 0x505AB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsGetRamChrMonster(uint mon_id);
    private static FhMethodHandle<MsGetRamChrMonster> _MsGetRamChrMonster =>
        new ( new FhMethodLocation("FFX.exe", 0x39AF00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsLimitUp(int param_1, Chr* character, uint init_limit_add);
    private static FhMethodHandle<MsLimitUp> _MsLimitUp =>
        new ( new FhMethodLocation("FFX.exe", 0x3B15A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Chr* MsGetChr(uint chr_id);
    private static FhMethodHandle<MsGetChr> _MsGetChr =>
        new ( new FhMethodLocation("FFX.exe", 0x394030) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsCalcWeakLevel(int current_hp, int max_hp);
    private static FhMethodHandle<MsCalcWeakLevel> _MsCalcWeakLevel =>
        new ( new FhMethodLocation("FFX.exe", 0x38BFC0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Command* MsGetRomPlyCommand(uint com_id, int* param_2);
    private static FhMethodHandle<MsGetRomPlyCommand> _MsGetRomPlyCommand =>
        new ( new FhMethodLocation("FFX.exe", 0x390AE0) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate byte TkMenuGetCurrentPlayer();
    private static FhMethodHandle<TkMenuGetCurrentPlayer> _TkMenuGetCurrentPlayer =>
        new ( new FhMethodLocation("FFX.exe", 0x4A9810) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ushort getScenerioFlag();
    private static FhMethodHandle<getScenerioFlag> _getScenerioFlag =>
        new ( new FhMethodLocation("FFX.exe", 0x387420) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate MsChrAbilityMap* MsGetChrAbilityMap(uint chr_id);
    private static FhMethodHandle<MsGetChrAbilityMap> _MsGetChrAbilityMap =>
        new ( new FhMethodLocation("FFX.exe", 0x398800) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void setCommandDisabled(int chr_id, int com_id, int is_disabled);
    private static FhMethodHandle<setCommandDisabled> _setCommandDisabled =>
        new ( new FhMethodLocation("FFX.exe", 0x39B480) );

    // Hooks
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CT_RetInt_0171_restoreCharHp(AtelBasicWorker* work, int* storage, AtelStack* stack);
    private static FhMethodHandle<CT_RetInt_0171_restoreCharHp> _CT_RetInt_0171_restoreCharHp =>
        new ( new FhMethodLocation("FFX.exe", 0x45C4F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CT_RetInt_0172_restoreCharMp(AtelBasicWorker* work, int* storage, AtelStack* stack);
    private static FhMethodHandle<CT_RetInt_0172_restoreCharMp> _CT_RetInt_0172_restoreCharMp =>
        new ( new FhMethodLocation("FFX.exe", 0x45C6B0) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void TkMenuDrawMain();
    private static FhMethodHandle<TkMenuDrawMain> _TkMenuDrawMain =>
        new ( new FhMethodLocation("FFX.exe", 0x4E0BA0) );

    [StructLayout(LayoutKind.Sequential)]
    public struct RGBA8
    {
        public byte r;
        public byte g;
        public byte b;
        public byte a;
    }
    private byte* p_toMenuNamePltNextH => FhUtil.ptr_at<byte  >(0x021D1670);
    private byte* p_DAT_025d1640       => FhUtil.ptr_at<byte  >(0x021D1640);
    private short* p_DAT_01871638      => FhUtil.ptr_at<short >(0x01471638);
    private ushort* p_DAT_00c56870     => FhUtil.ptr_at<ushort>(0x00856870);
    private byte* p_DAT_0187150c       => FhUtil.ptr_at<byte  >(0x0147150C);
    private int DAT_0187151c           => FhUtil.get_at<int   >(0x0147151C);
    private int DAT_01871520           => FhUtil.get_at<int   >(0x01471520);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008c0220(uint param_1, float param_2, float param_3, float param_4, float param_5);
    private static FhMethodHandle<FUN_008c0220> _FUN_008c0220 =>
        new ( new FhMethodLocation("FFX.exe", 0x4C0220) );

    [StructLayout(LayoutKind.Sequential)]
    public struct graphicDrawUIAbmapElement_param1
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
    private int TkFont_a => FhUtil.get_at<int>(0x01FCC470);
    private int TkFont_b => FhUtil.get_at<int>(0x01FCC468);
    private int TkFont_g => FhUtil.get_at<int>(0x01FCC460);
    private int TkFont_r => FhUtil.get_at<int>(0x01FCC458);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int AtelPushMember(AtelBasicWorker* work, int* storage, AtelStack* stack);
    private static FhMethodHandle<AtelPushMember> _AtelPushMember =>
        new ( new FhMethodLocation("FFX.exe", 0x46E2A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int AtelPopMember(AtelBasicWorker* work, int* storage, AtelStack* stack);
    private static FhMethodHandle<AtelPopMember> _AtelPopMember =>
        new ( new FhMethodLocation("FFX.exe", 0x46DD40) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008bc300(int param_1);
    private static FhMethodHandle<FUN_008bc300> _FUN_008bc300 =>
        new ( new FhMethodLocation("FFX.exe", 0x4BC300) );

    private int* p_DAT_01869ee4  => FhUtil.ptr_at<int >(0x01469EE4);
    private int* p_DAT_01869ee0  => FhUtil.ptr_at<int >(0x01469EE0);
    private byte* p_DAT_01869eea => FhUtil.ptr_at<byte>(0x01469EEA);
    private static byte* textString;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* MsWeaponName(int name_id, int owner, int hiragana, ushort* ref_model_id);
    private static FhMethodHandle<MsWeaponName> _MsWeaponName =>
        new ( new FhMethodLocation("FFX.exe", 0x3A0C70) );

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
        "Inhibitor",           // Silencestrike
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
    public delegate void FUN_008e67f0(uint param_1, float param_2, float param_3, float param_4);
    private static FhMethodHandle<FUN_008e67f0> _FUN_008e67f0 =>
        new ( new FhMethodLocation("FFX.exe", 0x4E67F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DrawCrossMenuIconWeaponName2(ushort* param_1, float param_2, float param_3, float param_4);
    private static FhMethodHandle<DrawCrossMenuIconWeaponName2> _DrawCrossMenuIconWeaponName2 =>
        new ( new FhMethodLocation("FFX.exe", 0x4E6970) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int TOBtlDrawCommandWindow(uint param_1);
    private static FhMethodHandle<TOBtlDrawCommandWindow> _TOBtlDrawCommandWindow =>
        new ( new FhMethodLocation("FFX.exe", 0x49F300) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008cf800(int param_1);
    private static FhMethodHandle<FUN_008cf800> _FUN_008cf800 =>
        new ( new FhMethodLocation("FFX.exe", 0x4CF800) );

    private int* DAT_0186a5ec    => FhUtil.ptr_at<int>(0x0146A5EC);
    private int* DAT_0186a5f0    => FhUtil.ptr_at<int>(0x0146A5F0);
    private int* TKMenuFaceRatio => FhUtil.ptr_at<int>(0x01FCC3C8);
    private int* TkMenuFaceKeep  => FhUtil.ptr_at<int>(0x01FCC3C4);
    private int* TkMenuFaceNew   => FhUtil.ptr_at<int>(0x01FCC3C0);
    private int* TkMenuFaceOld   => FhUtil.ptr_at<int>(0x01FCC3BC);
    private int* DAT_0186a634    => FhUtil.ptr_at<int>(0x0146A634);
    private int* DAT_0186a614    => FhUtil.ptr_at<int>(0x0146A614);
    private int* DAT_0186a654    => FhUtil.ptr_at<int>(0x0146A654);
    private int* DAT_0186a674    => FhUtil.ptr_at<int>(0x0146A674);
    private int* DAT_0186a5e4    => FhUtil.ptr_at<int>(0x0146A5E4);
    private int* DAT_0186a5d8    => FhUtil.ptr_at<int>(0x0146A5D8);
    private int* DAT_0186a5d4    => FhUtil.ptr_at<int>(0x0146A5D4);
    private static byte* scene0String;
    private static byte* scene11String;
    private static byte* scene20String;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsGetItemInternal_00798C20(int param_1, int param_2, int param_3);
    private static FhMethodHandle<MsGetItemInternal_00798C20> _MsGetItemInternal_00798C20 =>
        new ( new FhMethodLocation("FFX.exe", 0x398C20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MsChangeWeaponInvisible(uint param_1, byte param_2);
    private static FhMethodHandle<MsChangeWeaponInvisible> _MsChangeWeaponInvisible =>
        new ( new FhMethodLocation("FFX.exe", 0x3AD5F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_008d85f0(int param_1, int param_2);
    private static FhMethodHandle<FUN_008d85f0> _FUN_008d85f0 =>
        new ( new FhMethodLocation("FFX.exe", 0x4D85F0) );

    private uint DAT_0186ab60                              => FhUtil.get_at<uint         >(0x0146AB60);
    private int* p_DAT_0186aadc_curShopIdx                 => FhUtil.ptr_at<int          >(0x0146AADC);
    private int* p_DAT_0186ab68_arms_shop_bin_ptr          => FhUtil.ptr_at<int          >(0x0146AB68);
    private OverdriveMenu* p_DAT_01597730_OvrModesMenuList => FhUtil.ptr_at<OverdriveMenu>(0x01197730);
    [StructLayout(LayoutKind.Sequential)]
    public struct OverdriveMenu
    {
        public ushort overdrive_id;
        public byte type;
        public byte field2_0x3;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsLimitTypeDamageCheck(uint param_1, int param_2, uint param_3, int param_4, int param_5, int param_6, int param_7);
    private static FhMethodHandle<MsLimitTypeDamageCheck> _MsLimitTypeDamageCheck =>
        new ( new FhMethodLocation("FFX.exe", 0x3B0D60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsLimitTypeDeathCheck(int param_1, int param_2, uint param_3, int param_4);
    private static FhMethodHandle<MsLimitTypeDeathCheck> _MsLimitTypeDeathCheck =>
        new ( new FhMethodLocation("FFX.exe", 0x3B0F90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_007b10d0(uint chr_id, uint limit_mode, int param_3);
    private static FhMethodHandle<FUN_007b10d0> _FUN_007b10d0 =>
        new ( new FhMethodLocation("FFX.exe", 0x3B10D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsLimitTypeTurnCheck(uint param_1, int param_2);
    private static FhMethodHandle<MsLimitTypeTurnCheck> _MsLimitTypeTurnCheck =>
        new ( new FhMethodLocation("FFX.exe", 0x3B13D0) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int MsLimitTypeWinCheck();
    private static FhMethodHandle<MsLimitTypeWinCheck> _MsLimitTypeWinCheck =>
        new ( new FhMethodLocation("FFX.exe", 0x3B1550) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void MsSetSaveStartGame();
    private static FhMethodHandle<MsSetSaveStartGame> _MsSetSaveStartGame =>
        new ( new FhMethodLocation("FFX.exe", 0x386BC0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_00635c20(uint param_1);
    private static FhMethodHandle<FUN_00635c20> _FUN_00635c20 =>
        new ( new FhMethodLocation("FFX.exe", 0x235C20) );
        private int g_eventId => FhUtil.get_at<int>(0x00EFBBF8);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsParseCommand(byte* param_1);
    private static FhMethodHandle<MsParseCommand> _MsParseCommand =>
        new ( new FhMethodLocation("FFX.exe", 0x3AE380) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate void TOBtlCtrlHelpWin(int param_1);
    private static FhMethodHandle<TOBtlCtrlHelpWin> _TOBtlCtrlHelpWin =>
        new ( new FhMethodLocation("FFX.exe", 0x491250) );

    private byte* toBwNum => FhUtil.ptr_at<byte>(0x01fcc092);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort* TOGetSaveWindow(uint chr_id, BtlWindowType window_type, int* summonlistlength);
    private static FhMethodHandle<TOGetSaveWindow> _TOGetSaveWindow =>
        new ( new FhMethodLocation("FFX.exe", 0x49B510) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int TkMenuSummonEnableMask();
    private static FhMethodHandle<TkMenuSummonEnableMask> _TkMenuSummonEnableMask =>
        new ( new FhMethodLocation("FFX.exe", 0x4AB190) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MsSetSaveParam(uint chr_id);
    private static FhMethodHandle<MsSetSaveParam> _MsSetSaveParam =>
        new ( new FhMethodLocation("FFX.exe", 0x3861B0) );
        private static uint aeon = 0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int* FUN_00785c20(uint chr_id, uint* param_2);
    private static FhMethodHandle<FUN_00785c20> _FUN_00785c20 =>
        new ( new FhMethodLocation("FFX.exe", 0x385C20) );

    public struct MsChrAbilityMap
    {
        public int hp;
        public int mp;
        public byte strength;
        public byte defense;
        public byte magic;
        public byte magic_defense;
        public byte agility;
        public byte luck;
        public byte evasion;
        public byte accuracy;
        public AbilityInlineArray abilities;
    }

    [InlineArray(6)]
    public struct AbilityInlineArray
    {
        private ushort _data;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MsBtlReadManage();
    private static FhMethodHandle<MsBtlReadManage> _MsBtlReadManage =>
        new ( new FhMethodLocation("FFX.exe", 0x3830D0) );

    private ushort* _NkSeymourLegend = FhUtil.ptr_at<ushort>(0x00886D80);

    public override bool init(FhModContext mod_context, FileStream global_state_file)
    {
        _NkSeymourLegend[0] = 0x8019; // Break Damage Limit
        _NkSeymourLegend[1] = 0x800F; // Triple Overdrive
        _NkSeymourLegend[2] = 0x8006; // Magic Booster
        _NkSeymourLegend[3] = 0x800D; // One MP Cost

        return _CT_RetInt_0171_restoreCharHp.hook(this, h_CT_RetInt_0171_restoreCharHp)
            && _CT_RetInt_0172_restoreCharMp.hook(this, h_CT_RetInt_0172_restoreCharMp)
            && _TkMenuDrawMain              .hook(this, h_TkMenuDrawMain)
            && _FUN_008c0220                .hook(this, h_FUN_008c0220)
            && _AtelPushMember              .hook(this, h_AtelPushMember)
            && _AtelPopMember               .hook(this, h_AtelPopMember)
            && _FUN_008bc300                .hook(this, h_FUN_008bc300)
            && _MsWeaponName                .hook(this, h_MsWeaponName)
            && _FUN_008e67f0                .hook(this, h_FUN_008e67f0)
            && _DrawCrossMenuIconWeaponName2.hook(this, h_DrawCrossMenuIconWeaponName2)
            && _TOBtlDrawCommandWindow      .hook(this, h_TOBtlDrawCommandWindow)
            && _FUN_008cf800                .hook(this, h_FUN_008cf800)
            && _MsGetItemInternal_00798C20  .hook(this, h_MsGetItemInternal_00798C20)
            && _MsChangeWeaponInvisible     .hook(this, h_MsChangeWeaponInvisible)
            && _FUN_008d85f0                .hook(this, h_FUN_008d85f0)
            && _MsLimitTypeDamageCheck      .hook(this, h_MsLimitTypeDamageCheck)
            && _MsLimitTypeDeathCheck       .hook(this, h_MsLimitTypeDeathCheck)
            && _FUN_007b10d0                .hook(this, h_FUN_007b10d0)
            && _MsLimitTypeTurnCheck        .hook(this, h_MsLimitTypeTurnCheck)
            && _MsLimitTypeWinCheck         .hook(this, h_MsLimitTypeWinCheck)
            && _MsSetSaveStartGame          .hook(this, h_MsSetSaveStartGame)
            && _FUN_00635c20                .hook(this, h_FUN_00635c20)
            && _MsParseCommand              .hook(this, h_MsParseCommand)
            && _TOBtlCtrlHelpWin            .hook(this, h_TOBtlCtrlHelpWin)
            && _TOGetSaveWindow             .hook(this, h_TOGetSaveWindow)
            && _TkMenuSummonEnableMask      .hook(this, h_TkMenuSummonEnableMask)
            && _MsSetSaveParam              .hook(this, h_MsSetSaveParam)
            && _FUN_00785c20                .hook(this, h_FUN_00785c20)
            && _MsBtlReadManage             .hook(this, h_MsBtlReadManage);
    }
    public override void load_local_state(FileStream? local_state_file, FhLocalStateInfo local_state_info) { }
    public override void save_local_state(FileStream  local_state_file)                                    { }

    // If Kimahri gets restored, so does Seymour
    int h_CT_RetInt_0171_restoreCharHp(AtelBasicWorker* work, int* storage, AtelStack* stack)
    {
        uint chr_id;
        PlySave* ply_save;
        PlySave* seymour;

        chr_id = (uint)_AtelPopStackInteger.fnptr!(work, stack);
        ply_save = _MsGetSavePlayerPtr.fnptr!(chr_id);
        ply_save->battles_until_recovery = 0;
        if (chr_id == 3)
        {
            seymour = _MsGetSavePlayerPtr.fnptr!(7);
            seymour->battles_until_recovery = 0;
            seymour->hp = seymour->max_hp;
        }
        if (ply_save->max_hp < ply_save->hp)
        {
            return (int)ply_save->hp;
        }
        ply_save->hp = ply_save->max_hp;
        return (int)ply_save->max_hp;
    }

    int h_CT_RetInt_0172_restoreCharMp(AtelBasicWorker* work, int* storage, AtelStack* stack)
    {
        uint chr_id;
        PlySave* ply_save;
        PlySave* seymour;

        chr_id = (uint)_AtelPopStackInteger.fnptr!(work, stack);
        ply_save = _MsGetSavePlayerPtr.fnptr!(chr_id);
        if (chr_id == 3)
        {
            seymour = _MsGetSavePlayerPtr.fnptr!(7);
            seymour->mp = seymour->max_mp;
        }
        if (ply_save->max_mp < ply_save->mp)
        {
            return (int)ply_save->mp;
        }
        ply_save->mp = ply_save->max_mp;
        return (int)ply_save->max_mp;
    }

    // Party Menu
    void h_TkMenuDrawMain()
    {
        byte bVar1;
        int iVar2;
        int iVar3;
        uint uVar4;
        byte* pbVar5;
        AtelStack* pAVar7;
        float fVar8;
        float fVar9;
        float fVar10;
        float fVar11;
        float fVar12;
        float fVar13;
        int* storage;
        float uVar14;
        float fVar15;
        float uVar16;
        float uVar17;
        float fVar18;
        float* pfVar19;
        float fVar20;
        byte* puVar21;
        uint uVar22;
        RGBA8 color_1;
        uint color_end;
        RGBA8 color_2;
        float local_54;
        float local_50;
        float local_4c;
        float local_48;
        float local_44;
        float local_40;
        float local_3c;
        float local_38;
        double local_34;
        float local_2c;
        float local_28;
        float local_24;
        double local_20;
        byte[] local_18 = new byte[16];
        byte[] HP;
        byte[] MP;

        local_38 = 0.0f;
        local_40 = 0.0f;
        local_3c = 0.0f;
        local_44 = 0.0f;
        local_48 = 0.0f;
        local_4c = 0.0f;
        local_50 = 0.0f;
        iVar2 = (int)_TkMenuGetPlayerListMax2.fnptr!();
        _TkVU1SyncPath.fnptr!();
        _TOMenuOpenPktBuffTmp.fnptr!();
        pAVar7 = (AtelStack*)(nint)_graphicUiRemapX2.fnptr!(145.0f);
        _TODrawMenuBG.fnptr!();
        *p_toMenuNamePltNextH = 0x2c;
        *p_DAT_025d1640 = 0x20;
        local_24 = 0.0f;
        if (0 < iVar2)
        {
            do
            {
                fVar13 = local_24;
                local_2c = (float)(int)(short)(p_DAT_01871638)[(int)local_24];
                if (local_2c != 0.0)
                {
                    iVar3 = (int)(local_2c);
                    uVar4 = (uint)_FUN_0088e6c0.fnptr!(iVar3);
                    local_2c = (((float)(uVar4 & 0xffff) * -512.0f * 2.0f) / 12288.0f);
                    uVar4 = _TkMenuGetPlayerFromIndex2.fnptr!((int)fVar13);
                    local_24 = (float)(int)local_24;
                    fVar8 = _graphicUiRemapY2.fnptr!(80.0f); // Spacing between each character (originally 90.0f)
                    local_34 = (double)(fVar8 * local_24);
                    local_28 = _graphicUiRemapY2.fnptr!(200.0f);
                    local_28 = local_28 + (float)local_34;
                    local_34 = (double)(local_2c + (float)(nint)pAVar7);
                    if (local_24 >= 7) // Characters >= 7 use one of the 3 extra background plates in menu_new
                    {
                        fVar11 = local_24 * 86.0f + 170.0f;
                        fVar8 = local_24 * 86.0f + 98.0f;
                    }
                    else
                    {
                        fVar11 = local_24 * 86.0f + 72.0f;
                        fVar8 = local_24 * 86.0f + 0.0f;
                    }
                    fVar20 = 1109.0f;
                    fVar15 = 0.0f;
                    local_24 = fVar8;
                    fVar9 = _graphicUiRemapY2.fnptr!(72.0f);
                    fVar10 = _graphicUiRemapX2.fnptr!(1109.0f);
                    fVar12 = local_28;
                    local_24 = _graphicUiRemapX2.fnptr!(0.0f);
                    local_24 = local_24 + (float)local_34;
                    _TOMkpShapeXYWHUV.fnptr!(-3, local_24, fVar12, fVar10, fVar9, fVar15, fVar8, fVar20, fVar11);
                    iVar3 = (int)_FUN_008a9b20.fnptr!();
                    if ((int)fVar13 < iVar3) // Draw animated texture for frontline members
                    {
                        uVar22 = 0x1ac56870;
                        puVar21 = (byte*)p_DAT_00c56870;
                        fVar8 = (float)((int)fVar13 + 1) * 64.0f;
                        local_24 = (float)local_34;
                        uVar17 = -20.0f; ;
                        uVar16 = 50.0f;
                        fVar10 = 64.0f;
                        fVar9 = 250.0f;
                        uVar14 = 0;
                        local_2c = fVar8;
                        fVar11 = _graphicUiRemapY2.fnptr!(64.0f);
                        fVar12 = _graphicUiRemapX2.fnptr!(250.0f);
                        _FUN_008e7d30.fnptr!(local_24, local_28, fVar12, fVar11, uVar14, fVar8, fVar9, fVar10, uVar16, uVar17, (uint)puVar21, uVar22);
                        puVar21 = (byte*)p_DAT_00c56870;
                        uVar22 = 0x1ac56870;
                        uVar17 = -20.0f; ;
                        uVar16 = 50.0f;
                        fVar20 = 64.0f;
                        fVar15 = 250.0f;
                        uVar14 = (int)250.0f;
                        fVar11 = local_2c;
                        fVar12 = _graphicUiRemapY2.fnptr!(64.0f);
                        fVar9 = _graphicUiRemapX2.fnptr!(250.0f);
                        fVar8 = local_28;
                        fVar10 = _graphicUiRemapX2.fnptr!(250.0f);
                        _FUN_008e7d30.fnptr!(fVar10 + (float)local_34, fVar8, fVar9, fVar12, uVar14, fVar11, fVar15, fVar20, uVar16, uVar17, uVar22, (uint)puVar21);
                        uVar22 = 0x33c56870;
                        puVar21 = (byte*)p_DAT_00c56870;
                        uVar17 = -10.0f;
                        uVar16 = -80.0f;
                        fVar10 = 64.0f;
                        fVar9 = 250.0f;
                        uVar14 = (int)300.0f;
                        fVar8 = local_2c;
                        fVar11 = _graphicUiRemapY2.fnptr!(64.0f);
                        fVar12 = _graphicUiRemapX2.fnptr!(250.0f);
                        _FUN_008e7d30.fnptr!(local_24, local_28, fVar12, fVar11, uVar14, fVar8, fVar9, fVar10, uVar16, uVar17, (uint)puVar21, uVar22);
                        puVar21 = (byte*)p_DAT_00c56870;
                        uVar22 = 0x33c56870;
                        uVar17 = -10.0f;
                        uVar16 = -80.0f;
                        fVar20 = 64.0f;
                        fVar15 = 250.0f;
                        uVar14 = (int)550.0f;
                        fVar11 = local_2c;
                        fVar12 = _graphicUiRemapY2.fnptr!(64.0f);
                        fVar9 = _graphicUiRemapX2.fnptr!(250.0f);
                        fVar8 = local_28;
                        fVar10 = _graphicUiRemapX2.fnptr!(250.0f);
                        _FUN_008e7d30.fnptr!(fVar10 + (float)local_34, fVar8, fVar9, fVar12, uVar14, fVar11, fVar15, fVar20, uVar16, uVar17, uVar22, (uint)puVar21);
                    }
                    uVar14 = 0x3f800000;
                    fVar12 = 0.82f;
                    bVar1 = _FUN_008a9a20.fnptr!(uVar4);
                    fVar8 = _graphicUiRemapY2.fnptr!(9.0f);
                    fVar8 = fVar8 + local_28;
                    fVar11 = _graphicUiRemapX2.fnptr!(160.0f);
                    fVar11 = fVar11 + (float)local_34;
                    pbVar5 = _TOGetSaveChrName.fnptr!(uVar4);
                    _FUN_00905930.fnptr!(pbVar5, fVar11, fVar8, bVar1, fVar12, (int)uVar14);
                    fVar8 = (local_40 + 41.0f) * 0.0009765625f;
                    fVar11 = (local_38 + 436.0f) * 0.0009765625f;
                    fVar18 = 0.0146484375f;
                    fVar20 = 0.34765625f;
                    fVar9 = _graphicUiRemapY2.fnptr!(local_40 + 23.0f);
                    fVar10 = _graphicUiRemapX2.fnptr!(local_38 + 61.5f);
                    fVar12 = _graphicUiRemapY2.fnptr!(5.0f);
                    fVar12 = fVar12 + local_28;
                    fVar15 = _graphicUiRemapX2.fnptr!(490.0f);
                    _TOMkpShapeXYWHUV.fnptr!(0x21, fVar15 + (float)local_34, fVar12, fVar10, fVar9, fVar20, fVar18, fVar11, fVar8);
                    uVar14 = _FUN_008a9940.fnptr!(uVar4);
                    uVar16 = _FUN_008a9870.fnptr!(uVar4);
                    HP = Encoding.UTF8.GetBytes($"{uVar16,5}/{uVar14,5}");
                    fVar9 = 0.0f;
                    fVar12 = 0.72f;
                    bVar1 = 0x25;
                    fVar8 = _graphicUiRemapY2.fnptr!(6.0f);
                    fVar8 = fVar8 + local_28;
                    fVar11 = _graphicUiRemapX2.fnptr!(604.0f);
                    fVar11 = fVar11 + (float)local_34 + local_3c;
                    fixed (byte* HPDisplay = HP) _FUN_00901660.fnptr!(HPDisplay, fVar11, fVar8, bVar1, fVar12, fVar9);
                    fVar18 = 0.040039063f;
                    fVar8 = (local_38 + 570.0f) * 0.0009765625f;
                    fVar20 = 0.0146484375f;
                    fVar15 = 0.49804688f;
                    fVar12 = _graphicUiRemapY2.fnptr!(23.0f);
                    fVar9 = _graphicUiRemapX2.fnptr!(local_38 + 49.0f);
                    fVar11 = _graphicUiRemapY2.fnptr!(32.0f);
                    fVar11 = fVar11 + local_28;
                    fVar10 = _graphicUiRemapX2.fnptr!(540.0f);
                    _TOMkpShapeXYWHUV.fnptr!(0x22, fVar10 + (float)local_34, fVar11, fVar9, fVar12, fVar15, fVar20, fVar8, fVar18);
                    uVar14 = _FUN_008a9960.fnptr!(uVar4);
                    uVar16 = _FUN_008a9920.fnptr!(uVar4);
                    MP = Encoding.UTF8.GetBytes($"{uVar16,4}/{uVar14,4}");
                    fVar9 = 0.0f;
                    fVar12 = 0.72f;
                    bVar1 = 0x25;
                    fVar8 = _graphicUiRemapY2.fnptr!(34.0f);
                    fVar8 = fVar8 + local_28;
                    fVar11 = _graphicUiRemapX2.fnptr!(625.0f);
                    fVar11 = fVar11 + (float)local_34 + local_3c;
                    fixed (byte* MPDisplay = MP) _FUN_00901660.fnptr!(MPDisplay, fVar11, fVar8, bVar1, fVar12, fVar9);
                    fVar18 = 636.0f;
                    fVar20 = 208.0f;
                    fVar15 = 603.0f;
                    fVar10 = 0.0f;
                    fVar11 = _graphicUiRemapY2.fnptr!(33.0f);
                    fVar12 = _graphicUiRemapX2.fnptr!(208.0f);
                    fVar8 = _graphicUiRemapY2.fnptr!(28.0f);
                    fVar8 = fVar8 + local_28;
                    fVar9 = _graphicUiRemapX2.fnptr!(844.0f);
                    _TOMkpShapeXYWHUV.fnptr!(-1, fVar9 + (float)local_34, fVar8, fVar12, fVar11, fVar10, fVar15, fVar20, fVar18);
                    fVar18 = 0.6621094f;
                    fVar20 = 0.5595703f;
                    fVar15 = 0.5888672f;
                    fVar10 = 0.48632813f;
                    fVar11 = _graphicUiRemapY2.fnptr!(75.0f);
                    fVar12 = _graphicUiRemapX2.fnptr!(75.0f);
                    local_20 = (double)local_28;
                    fVar8 = _graphicUiRemapY2.fnptr!(3.0f);
                    fVar8 = (float)local_20 - fVar8;
                    fVar9 = _graphicUiRemapX2.fnptr!(1038.0f);
                    _TOMkpShapeXYWHUV.fnptr!(0x3d, fVar9 + (float)local_34, fVar8, fVar12, fVar11, fVar10, fVar15, fVar20, fVar18);
                    fVar18 = 0.11328125f;
                    fVar8 = (local_48 + 978.0f) * 0.0009765625f;
                    fVar20 = 0.08203125f;
                    fVar11 = (local_44 + 834.0f) * 0.0009765625f;
                    fVar9 = _graphicUiRemapY2.fnptr!(26.0f);
                    fVar10 = _graphicUiRemapX2.fnptr!(local_50 + 117.0f);
                    fVar12 = _graphicUiRemapY2.fnptr!(35.0f);
                    fVar12 = fVar12 + local_28;
                    fVar15 = _graphicUiRemapX2.fnptr!(local_4c + 952.0f);
                    _TOMkpShapeXYWHUV.fnptr!(0x3d, fVar15 + (float)local_34, fVar12, fVar10, fVar9, fVar11, fVar20, fVar8, fVar18);
                    pfVar19 = &local_54;
                    iVar3 = _FUN_008a9b30.fnptr!((byte)uVar4);
                    _FUN_00905230.fnptr!(iVar3, pfVar19, 0.7f, 0.0f);
                    uVar14 = 0x3f800000;
                    fVar12 = 0.68f;
                    bVar1 = 0;
                    fVar8 = _graphicUiRemapY2.fnptr!(16.0f);
                    fVar8 = fVar8 + local_28;
                    fVar11 = _graphicUiRemapX2.fnptr!(1076.0f);
                    fVar11 = (fVar11 + (float)local_34) - local_54 * 0.5f;
                    iVar3 = _FUN_008a9b30.fnptr!((byte)uVar4);
                    _FUN_00905820.fnptr!(iVar3, fVar11, fVar8, bVar1, fVar12, uVar14);
                    if (*p_DAT_0187150c == uVar4)
                    {
                        fVar12 = 0.0f;
                        fVar8 = _graphicUiRemapY2.fnptr!(14.0f);
                        fVar8 = fVar8 + local_28;
                        fVar11 = _graphicUiRemapX2.fnptr!(150.0f);
                        _TkMn2DrawCrossCursor.fnptr!(fVar11 + (float)local_34, fVar8, fVar12);
                    }
                    if ((-1 < DAT_0187151c) && (_TkMenuGetPlayerFromIndex2.fnptr!(DAT_0187151c) == uVar4))
                    {
                        if (DAT_01871520 < 0)
                        {
                            fVar12 = 0.0f;
                            fVar8 = _graphicUiRemapY2.fnptr!(14.0f);
                            fVar8 = fVar8 + local_28;
                            fVar11 = _graphicUiRemapX2.fnptr!(150.0f);
                            _TkMn2DrawCrossCursor.fnptr!(fVar11 + (float)local_34, fVar8, fVar12);
                        }
                        else
                        {
                            iVar3 = _FUN_008a9c10.fnptr!();
                            if ((iVar3 / 2 & 1U) != 0)
                            {
                                iVar3 = 0;
                                fVar8 = _graphicUiRemapY2.fnptr!(9.0f);
                                fVar8 = fVar8 + local_28;
                                fVar11 = _graphicUiRemapX2.fnptr!(140.0f);
                                _FUN_008c13b0.fnptr!(fVar11 + (float)local_34, fVar8, iVar3);
                            }
                        }
                    }
                    if ((-1 < DAT_01871520) && (_TkMenuGetPlayerFromIndex2.fnptr!(DAT_01871520) == uVar4))
                    {
                        fVar12 = 0.0f;
                        fVar8 = _graphicUiRemapY2.fnptr!(14.0f);
                        fVar8 = fVar8 + local_28;
                        fVar11 = _graphicUiRemapX2.fnptr!(150.0f);
                        _TkMn2DrawCrossCursor.fnptr!(fVar11 + (float)local_34, fVar8, fVar12);
                    }
                }
                local_24 = (float)((int)fVar13 + 1);
            } while ((int)local_24 < iVar2);
        }
        uVar22 = 0x40ffffff;
        uVar4 = 0xffffff;
        fVar13 = _graphicUiRemapY2.fnptr!(32.0f);
        fVar8 = _graphicUiRemapX2.fnptr!(600.0f);
        fVar11 = _graphicUiRemapY2.fnptr!(870.0f);
        fVar12 = _graphicUiRemapX2.fnptr!(639.0f);
        _TODrawCrossBoxXYWHC2.fnptr!(fVar12, fVar11, fVar8, fVar13, uVar4, uVar22);
        iVar2 = _MsGetGIL.fnptr!((AtelBasicWorker*)200, (int*)(nint)_graphicUiRemapX2.fnptr!(145.0f), pAVar7);
        fVar13 = _graphicUiRemapY2.fnptr!(32.0f);
        fVar8 = _graphicUiRemapX2.fnptr!(1108.0f);
        fVar11 = _graphicUiRemapY2.fnptr!(870.0f);
        fVar12 = _graphicUiRemapX2.fnptr!(145.0f);
        _FUN_008c09f0.fnptr!(fVar12, fVar11, fVar8, fVar13, iVar2);
        uVar4 = (uint)_FUN_008a9c00.fnptr!();
        color_end = 0xffffff;
        uVar22 = 0x40ffffff;
        iVar2 = ((uVar4 & 1) != 0) ? 37 : 1;
        fVar13 = _graphicUiRemapY2.fnptr!(32.0f);
        fVar8 = _graphicUiRemapX2.fnptr!(325.0f);
        fVar11 = _graphicUiRemapY2.fnptr!(870.0f);
        fVar12 = _graphicUiRemapX2.fnptr!(145.0f);
        _TODrawCrossBoxXYWHC2.fnptr!(fVar12, fVar11, fVar8, fVar13, uVar22, color_end);
        bVar1 = 0x25;
        fVar13 = _graphicUiRemapY2.fnptr!(878.0f);
        fVar8 = _graphicUiRemapX2.fnptr!(270.0f);
        _FUN_008e19f0.fnptr!(uVar4, fVar8, fVar13, bVar1, iVar2);
        color_2.r = 0x80;
        color_2.g = 0x80;
        color_2.b = 0x80;
        color_2.a = 0;
        color_1.r = 0x80;
        color_1.g = 0x80;
        color_1.b = 0x80;
        color_1.a = 0x80;
        fVar20 = 599.0f;
        fVar15 = 1600.0f;
        fVar10 = 544.0f;
        fVar9 = 0.0f;
        fVar13 = _graphicUiRemapY2.fnptr!(55.0f);
        fVar8 = _graphicUiRemapX2.fnptr!(1130.0f);
        fVar11 = _graphicUiRemapY2.fnptr!(951.0f);
        fVar12 = _graphicUiRemapX2.fnptr!(145.0f);
        _TOMkpShapeXYWHUVC2.fnptr!(0xffffffff, fVar12, fVar11, fVar8, fVar13, fVar9, fVar10, fVar15, fVar20, color_1, color_2);
        fVar10 = 0.49316406f;
        fVar9 = 0.8808594f;
        fVar12 = 0.4658203f;
        fVar11 = 0.5292969f;
        fVar13 = _graphicUiRemapY2.fnptr!(28.0f);
        fVar8 = _graphicUiRemapX2.fnptr!(360.0f);
        pAVar7 = (AtelStack*)(nint)_graphicUiRemapY2.fnptr!(933.0f);
        storage = (int*)(nint)_graphicUiRemapX2.fnptr!(145.0f);
        _TOMkpShapeXYWHUV.fnptr!(200, (float)(nint)storage, (float)(nint)pAVar7, fVar8, fVar13, fVar11, fVar12, fVar9, fVar10);
        uVar4 = (uint)_AtelGetSaveDic.fnptr!((AtelBasicWorker*)200, storage, pAVar7);
        iVar2 = _MsGetSaveConfigEnglish.fnptr!();
        pbVar5 = (byte*)_AtelGetSaveDicName.fnptr!(uVar4, iVar2);
        _TkMenuDraw1612Width.fnptr!(pbVar5);
        fVar12 = 1.0f;
        fVar11 = 0.78f;
        bVar1 = 0;
        fVar13 = _graphicUiRemapY2.fnptr!(956.0f);
        fVar8 = _graphicUiRemapX2.fnptr!(235.0f);
        _TOMkpCrossExtMesFontLClut.fnptr!(0, pbVar5, fVar8, fVar13, bVar1, fVar11, fVar12);
        // Could move Playtime/Gil/Location down on the y coord to make space for extra characters?
        _TOMenuDrawKickTmp.fnptr!();
        _TkVU1SyncPath.fnptr!();
        return;
    }
    // Portrait in Menus
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
        graphicDrawUIAbmapElement_param1 local_a0 = new();
        float v;

        fVar4 = TkFont_a;
        fVar3 = TkFont_b;
        fVar2 = TkFont_g;
        fVar1 = TkFont_r;
        if (param_1 < 8)
        {
            local_a4 = _TOGetShapTextureName.fnptr!(0x2ed0);
            _TOGetImageWH.fnptr!(0x2ed0, &local_ac, &local_b0);
            if (param_1 == 7) // Seymour
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
                _graphicDrawUIElement.fnptr!(&local_a0, local_a4, 1, 0, 0);
                return;
            }
            else
            {
                if (param_1 == 6)
                {
                    iVar5 = _AtelGetAlbhedRikku.fnptr!();
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
            _graphicDrawUIElement.fnptr!(&local_a0, local_a4, 1, 0, 0);
            return;
        }
        pcVar6 = _TOGetShapTextureName.fnptr!(0x2ed4);
        _TOGetImageWH.fnptr!(0x2ed4, &local_ac, &local_b0);
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
        v = (float)((int)tex_path + 100);
        local_a0.floats1[3] = v / local_b0;
        local_a0.ints1[0] = (int)fVar4;
        local_a0.ints1[1] = (int)fVar3;
        local_a0.ints1[2] = (int)fVar2;
        local_a0.ints1[3] = (int)fVar1;
        _graphicDrawUIElement.fnptr!(&local_a0, pcVar6, 1, 0, 0);
        return;
    }

    // Save Party Lineup
    int h_AtelPushMember(AtelBasicWorker* work, int* storage, AtelStack* stack)
    {
        byte bVar1;
        SaveData* pSVar2;
        bool BVar3;
        byte* puVar4;
        uint* puVar5;
        int iVar6;
        uint uVar7;
        uint chr_id;
        uint* local_14 = stackalloc uint[4];

        pSVar2 = _AtelGetEventSaveRamAdrs.fnptr!();
        bVar1 = pSVar2->atel_is_push_member;
        _MsGetSavePartyMember.fnptr!(local_14, local_14 + 1, local_14 + 2);
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
            BVar3 = _MsGetSavePlyJoin.fnptr!(chr_id);
            if (BVar3)
            {
                *(uint*)&pSVar2->atel_push_party = *(uint*)&pSVar2->atel_push_party | uVar7;
            }
            uVar7 = uVar7 << 1 | (uint)((int)uVar7 < 0 ? 1 : 0);
            chr_id = chr_id + 1;
        } while ((int)chr_id < 8);
        pSVar2->atel_is_push_member = 1;
        return bVar1;
    }
    // Restore Party Lineup
    int h_AtelPopMember(AtelBasicWorker* work, int* storage, AtelStack* stack)
    {
        byte bVar1;
        SaveData* pSVar2;
        uint uVar3;
        uint uVar4;
        uint uVar5;

        pSVar2 = _AtelGetEventSaveRamAdrs.fnptr!();
        bVar1 = pSVar2->atel_is_push_member;
        if (bVar1 != 0)
        {
            uVar4 = 0;
            uVar5 = 1;
            do
            {
                _MsSetSavePlyJoin.fnptr!(uVar4, (int)(((*(uint*)&pSVar2->atel_push_party & uVar5) != 0) ? 1u : 0u));
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
            _FUN_00786a10.fnptr!(uVar4, uVar5, uVar3);
        }
        pSVar2->atel_is_push_member = 0;
        return bVar1;
    }

    // Battle Results: AP Earned
    void h_FUN_008bc300(int param_1)
    {
        byte* name;
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
                uVar18 = (uint)_FUN_0088e6c0.fnptr!(*(short*)((int)p_DAT_01869ee0 + param_1 * 0xe + 2));
                iVar2 = (int)((uVar18 & 0xffff) * -0x200);
                goto LAB_008bc382;
            case 2:
            case 3:
                local_c = 0.0f;
                break;
            case 4:
                uVar18 = (uint)_FUN_0088e6a0.fnptr!(0x1000 - *(short*)((int)p_DAT_01869ee0 + param_1 * 0xe + 2));
                iVar2 = (int)(((uVar18 & 0xffff) - 0x1000) * 0x200);
            LAB_008bc382:
                local_c = (int)(iVar2 + (iVar2 >> 0x1f & 0xfffU)) >> 0xc;
                break;
        }
        _TOMenuOpenPktBuffTmp.fnptr!();
        fVar4 = _graphicUiRemapX2.fnptr!(210.0f);
        local_c = fVar4 + local_c;
        local_8 = param_1 * 0x50 + 0x10c; // Spacing between each character (originally param_1 * 0x5a + 0x116)
        local_8 = _graphicUiRemapY2.fnptr!(local_8);
        if (param_1 >= 7) // Characters >= 7 use one of the 3 extra background plates in menu_new
        {
            fVar4 = param_1 * 86.0f + 170.0f;
            fVar7 = param_1 * 86.0f + 98.0f;
        }
        else
        {
            fVar4 = param_1 * 86.0f + 72.0f;
            fVar7 = param_1 * 86.0f + 0.0f;
        }
        fVar16 = 1110.0f;
        fVar11 = 0.0f;
        fVar5 = _graphicUiRemapY2.fnptr!(72.0f);
        fVar6 = _graphicUiRemapX2.fnptr!(1110.0f);
        _TOMkpShapeXYWHUV.fnptr!(-3, local_c, local_8, fVar6, fVar5, fVar11, fVar7, fVar16, fVar4);
        uVar18 = 0x1ac56870;
        puVar17 = (byte*)p_DAT_00c56870;
        fVar4 = (param_1 + 1) * 64.0f;
        uVar14 = -20.0f;
        uVar12 = 50.0f;
        fVar16 = 64.0f;
        fVar11 = 250.0f;
        uVar8 = 0;
        fVar7 = fVar4;
        fVar5 = _graphicUiRemapY2.fnptr!(64.0f);
        fVar6 = _graphicUiRemapX2.fnptr!(250.0f);
        _FUN_008e7d30.fnptr!(local_c, local_8, fVar6, fVar5, uVar8, fVar7, fVar11, fVar16, uVar12, uVar14, (uint)puVar17, uVar18);
        puVar17 = (byte*)p_DAT_00c56870;
        uVar18 = 0x1ac56870;
        uVar14 = -20.0f;
        uVar12 = 50.0f;
        fVar10 = 64.0f;
        fVar9 = 250.0f;
        uVar8 = (int)250.0f;
        fVar5 = fVar4;
        fVar6 = _graphicUiRemapY2.fnptr!(64.0f);
        fVar11 = _graphicUiRemapX2.fnptr!(250.0f);
        fVar7 = local_8;
        fVar16 = _graphicUiRemapX2.fnptr!(250.0f);
        local_28 = (double)(fVar16 + local_c);
        _FUN_008e7d30.fnptr!(fVar16 + local_c, fVar7, fVar11, fVar6, uVar8, fVar5, fVar9, fVar10, uVar12, uVar14, uVar18, (uint)puVar17);
        uVar18 = 0x33c56870;
        puVar17 = (byte*)p_DAT_00c56870;
        uVar14 = -10.0f;
        uVar12 = -80.0f;
        fVar16 = 64.0f;
        fVar11 = 250.0f;
        uVar8 = (int)300.0f;
        fVar7 = fVar4;
        fVar5 = _graphicUiRemapY2.fnptr!(64.0f);
        fVar6 = _graphicUiRemapX2.fnptr!(250.0f);
        _FUN_008e7d30.fnptr!(local_c, local_8, fVar6, fVar5, uVar8, fVar7, fVar11, fVar16, uVar12, uVar14, (uint)puVar17, uVar18);
        puVar17 = (byte*)p_DAT_00c56870;
        uVar18 = 0x33c56870;
        uVar14 = -10.0f;
        uVar12 = -80.0f;
        fVar9 = 64.0f;
        fVar16 = 250.0f;
        uVar8 = (int)550.0f;
        fVar5 = _graphicUiRemapY2.fnptr!(64.0f);
        fVar6 = _graphicUiRemapX2.fnptr!(250.0f);
        fVar7 = local_8;
        fVar11 = _graphicUiRemapX2.fnptr!(250.0f);
        _FUN_008e7d30.fnptr!(fVar11 + local_c, fVar7, fVar6, fVar5, uVar8, fVar4, fVar16, fVar9, uVar12, uVar14, uVar18, (uint)puVar17);
        fVar10 = 688.0f;
        fVar9 = 1104.0f;
        fVar16 = 618.0f;
        fVar11 = 689.0f;
        fVar7 = _graphicUiRemapY2.fnptr!(70.0f);
        fVar5 = _graphicUiRemapX2.fnptr!(415.0f);
        fVar4 = local_8;
        fVar6 = _graphicUiRemapX2.fnptr!(1090.0f);
        _TOMkpShapeXYWHUV.fnptr!(-3, fVar6 + local_c, fVar4, fVar5, fVar7, fVar11, fVar16, fVar9, fVar10);
        uVar8 = 0x3f800000;
        fVar5 = 0.82f;
        bVar13 = 0;
        fVar4 = _graphicUiRemapY2.fnptr!(9.0f);
        fVar4 = fVar4 + local_8;
        fVar7 = _graphicUiRemapX2.fnptr!(160.0f);
        fVar7 = fVar7 + local_c;
        name = _TOGetSaveChrName.fnptr!((uint)(int)*(short*)((int)p_DAT_01869ee4 + param_1 * 0xe + 2));
        _FUN_00905930.fnptr!(name, fVar7, fVar4, bVar13, fVar5, uVar8);
        fVar10 = 0.10253906f;
        fVar4 = (local_10 + 434.0f) * 0.0009765625f;
        fVar9 = 0.07519531f;
        fVar16 = 0.37304688f;
        fVar5 = _graphicUiRemapY2.fnptr!(28.0f);
        fVar6 = _graphicUiRemapX2.fnptr!(local_10 + 55.0f);
        fVar7 = _graphicUiRemapY2.fnptr!(33.0f);
        fVar7 = fVar7 + local_8;
        fVar11 = _graphicUiRemapX2.fnptr!(local_20 + 758.0f);
        _TOMkpShapeXYWHUV.fnptr!(200, fVar11 + local_c, fVar7, fVar6, fVar5, fVar16, fVar9, fVar4, fVar10);
        uVar12 = 0;
        fVar6 = 0.7f;
        uVar8 = 0x25;
        fVar4 = _graphicUiRemapY2.fnptr!(27.0f);
        fVar4 = fVar4 + local_8;
        fVar7 = _graphicUiRemapX2.fnptr!(local_20 + 748.0f);
        fVar7 = fVar7 + local_c;
        fVar5 = _FUN_008bd9d0.fnptr!(*(short*)((int)p_DAT_01869ee4 + param_1 * 0xe + 2));
        _FUN_009055c0.fnptr!((int)fVar5, fVar7, fVar4, uVar8, fVar6, uVar12);
        fVar10 = 0.15234375f;
        fVar9 = 0.7421875f;
        fVar16 = 0.12402344f;
        fVar11 = 0.3671875f;
        fVar7 = _graphicUiRemapY2.fnptr!(28.0f);
        fVar5 = _graphicUiRemapX2.fnptr!(384.0f);
        fVar4 = _graphicUiRemapY2.fnptr!(2.0f);
        fVar4 = fVar4 + local_8;
        fVar6 = _graphicUiRemapX2.fnptr!(1096.0f);
        _TOMkpShapeXYWHUV.fnptr!(200, fVar6 + local_c, fVar4, fVar5, fVar7, fVar11, fVar16, fVar9, fVar10);
        fVar10 = 636.0f;
        fVar9 = 208.0f;
        fVar16 = 603.0f;
        fVar11 = 0.0f;
        fVar7 = _graphicUiRemapY2.fnptr!(33.0f);
        fVar5 = _graphicUiRemapX2.fnptr!(208.0f);
        fVar4 = _graphicUiRemapY2.fnptr!(29.0f);
        fVar4 = fVar4 + local_8;
        fVar6 = _graphicUiRemapX2.fnptr!(844.0f);
        _TOMkpShapeXYWHUV.fnptr!(-1, fVar6 + local_c, fVar4, fVar5, fVar7, fVar11, fVar16, fVar9, fVar10);
        fVar10 = 0.6621094f;
        fVar9 = 0.5595703f;
        fVar16 = 0.5888672f;
        fVar11 = 0.48632813f;
        fVar7 = _graphicUiRemapY2.fnptr!(75.0f);
        fVar5 = _graphicUiRemapX2.fnptr!(75.0f);
        local_28 = local_8;
        fVar4 = _graphicUiRemapY2.fnptr!(3.0f);
        fVar4 = (float)local_28 - fVar4;
        fVar6 = _graphicUiRemapX2.fnptr!(1038.0f);
        _TOMkpShapeXYWHUV.fnptr!(0x3d, fVar6 + local_c, fVar4, fVar5, fVar7, fVar11, fVar16, fVar9, fVar10);
        fVar10 = 0.11328125f;
        fVar4 = (local_10 + 978.0f) * 0.0009765625f;
        fVar9 = 0.08203125f;
        fVar7 = (local_14 + 834.0f) * 0.0009765625f;
        fVar6 = _graphicUiRemapY2.fnptr!(32.0f);
        fVar11 = _graphicUiRemapX2.fnptr!(local_18 + 144.0f);
        fVar5 = _graphicUiRemapY2.fnptr!(32.0f);
        fVar5 = fVar5 + local_8;
        fVar16 = _graphicUiRemapX2.fnptr!(local_1c + 935.0f);
        _TOMkpShapeXYWHUV.fnptr!(0x3d, fVar16 + local_c, fVar5, fVar11, fVar6, fVar7, fVar9, fVar4, fVar10);
        pfVar15 = &local_2c;
        iVar2 = _FUN_008a9b30.fnptr!((byte)*(ushort*)((int)p_DAT_01869ee4 + param_1 * 0xe + 2));
        _FUN_00905230.fnptr!(iVar2, pfVar15, 0.7f, 0.0f);
        fVar5 = 0.7f;
        bVar13 = 0;
        fVar4 = _graphicUiRemapY2.fnptr!(16.0f);
        fVar4 = fVar4 + local_8;
        fVar7 = _graphicUiRemapX2.fnptr!(1076.0f);
        fVar7 = (fVar7 + local_c - local_2c * 0.5f);
        iVar2 = _FUN_008a9b30.fnptr!((byte)*(ushort*)((int)p_DAT_01869ee4 + param_1 * 0xe + 2));
        _FUN_00905550.fnptr!(iVar2, fVar7, fVar4, bVar13, fVar5);
        fVar10 = 0.10253906f;
        fVar4 = (local_10 + 434.0f) * 0.0009765625f;
        fVar9 = 0.07519531f;
        fVar16 = 0.37304688f;
        fVar5 = _graphicUiRemapY2.fnptr!(28.0f);
        fVar6 = _graphicUiRemapX2.fnptr!(local_10 + 55.0f);
        fVar7 = _graphicUiRemapY2.fnptr!(34.0f);
        fVar7 = fVar7 + local_8;
        fVar11 = _graphicUiRemapX2.fnptr!(1348.0f);
        _TOMkpShapeXYWHUV.fnptr!(200, fVar11 + local_c, fVar7, fVar6, fVar5, fVar16, fVar9, fVar4, fVar10);
        iVar2 = _FUN_008bda10.fnptr!((byte)*(ushort*)((int)p_DAT_01869ee4 + param_1 * 0xe + 2));
        fVar7 = 0.0f;
        fVar4 = 0.7f;
        uVar8 = 0x25;
        if (iVar2 < 99)
        {
            fVar5 = _graphicUiRemapY2.fnptr!(28.0f);
            fVar5 = fVar5 + local_8;
            fVar6 = _graphicUiRemapX2.fnptr!(1333.0f);
            fVar6 = fVar6 + local_c;
            iVar2 = _MsGetNextAP.fnptr!(*(short*)((int)p_DAT_01869ee4 + param_1 * 0xe + 2));
            iVar3 = _FUN_00785370.fnptr!((byte)*(short*)((int)p_DAT_01869ee4 + param_1 * 0xe + 2));
            _FUN_009055c0.fnptr!(iVar2 - iVar3, fVar6, fVar5, uVar8, fVar4, fVar7);
        }
        else
        {
            fVar5 = _graphicUiRemapY2.fnptr!(38.0f);
            bVar13 = (byte)uVar8;
            fVar5 = fVar5 + local_8;
            fVar6 = _graphicUiRemapX2.fnptr!(1210.0f);
            _FUN_00901660.fnptr!(textString, fVar6 + local_c, fVar5, bVar13, fVar4, fVar7);
        }
        if (0 < p_DAT_01869eea[param_1 * 0xe])
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
            fVar5 = _graphicUiRemapY2.fnptr!(54.0f);
            fVar4 = _graphicUiRemapX2.fnptr!(100.0f);
            fVar4 = fVar4 * (int)(uVar18 + 1);
            fVar7 = _graphicUiRemapY2.fnptr!(32.0f);
            fVar7 = fVar7 + local_8;
            fVar6 = _graphicUiRemapX2.fnptr!(1110.0f);
            _TODrawMenuPlateXYWHType.fnptr!(fVar6 + local_c, fVar7, fVar4, fVar5, iVar2);
            if (uVar18 == 2)
            {
                fVar10 = 0.11328125f;
                fVar4 = (local_10 + 978.0f) * 0.0009765625f;
                fVar9 = 0.08203125f;
                fVar7 = (local_14 + 834.0f) * 0.0009765625f;
                fVar6 = _graphicUiRemapY2.fnptr!(32.0f);
                fVar11 = _graphicUiRemapX2.fnptr!(local_18 + 144.0f);
                fVar5 = _graphicUiRemapY2.fnptr!(43.0f);
                fVar5 = fVar5 + local_8;
                fVar16 = _graphicUiRemapX2.fnptr!(local_1c + 1152.0f);
                _TOMkpShapeXYWHUV.fnptr!(0x3d, fVar16 + local_c, fVar5, fVar11, fVar6, fVar7, fVar9, fVar4, fVar10);
                fVar10 = 0.6386719f;
                fVar4 = (local_10 + 680.0f) * 0.0009765625f;
                fVar9 = 0.6064453f;
                fVar7 = (local_14 + 614.0f) * 0.0009765625f;
                fVar6 = _graphicUiRemapY2.fnptr!(33.0f);
                fVar11 = _graphicUiRemapX2.fnptr!(local_18 + 66.0f);
                fVar5 = _graphicUiRemapY2.fnptr!(43.0f);
                fVar5 = fVar5 + local_8;
                fVar16 = _graphicUiRemapX2.fnptr!(local_1c + 1265.0f);
                _TOMkpShapeXYWHUV.fnptr!(0x3d, fVar16 + local_c, fVar5, fVar11, fVar6, fVar7, fVar9, fVar4, fVar10);
            }
            p_DAT_01869eea[param_1 * 0xe] = (byte)(p_DAT_01869eea[param_1 * 0xe] + -1);
        }
        _TOMenuDrawKickTmp.fnptr!();
    switchD_008bc339_caseD_0:
        return;
    }

    // Seymour Gear Names
    byte* h_MsWeaponName(int name_id, int owner, int simplified, ushort* ref_model_id)
    {
        if (owner != 7)
        {
            return _MsWeaponName.chain_from(h_MsWeaponName).fnptr!(name_id, owner, simplified, ref_model_id);
        }
        int gearid = name_id & 0xFFF;
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
        {
            return _MsWeaponName.chain_from(h_MsWeaponName).fnptr!(name_id, owner, simplified, ref_model_id);
        }
        return (byte*)seymour_gear_names[gearid];
    }
    // Equipment Names + Icons for Swap/Discard, Equip & Customize Menus
    void h_FUN_008e67f0(uint param_1, float param_2, float param_3, float param_4)
    {
        Equipment* pSVar1;
        byte* pbVar2;
        byte bVar3;
        float fVar4;
        float fVar5;
        float fVar6;
        float fVar7;
        byte bVar8;
        byte bVar9;
        byte bVar10;
        byte bVar11;
        byte* local_c;
        float local_8;

        pSVar1 = _MsGetSaveWeapon.fnptr!(param_1, &local_c);
        pbVar2 = _MsGetSaveWeaponName.fnptr!(param_1);
        bVar11 = 0x80;
        if (pSVar1->owner == 7)
        {
            bVar3 = (byte)(37 * 2 + 2 + pSVar1->type);
        }
        else
        {
            bVar3 = (byte)(pSVar1->owner * 2 + 1 + pSVar1->type);
        }
        bVar10 = 0x80;
        bVar9 = 0x80;
        bVar8 = 0x80;
        fVar4 = _graphicUiRemapY2.fnptr!(46.0f);
        fVar5 = _graphicUiRemapX2.fnptr!(38.0f);
        fVar6 = _graphicUiRemapY2.fnptr!(7.0f);
        fVar6 = fVar6 + param_3;
        local_8 = fVar6;
        local_8 = _graphicUiRemapX2.fnptr!(180.0f);
        local_8 = local_8 + param_2;
        _DrawCrossMenuIconXYWHRGBA.fnptr!(local_8, fVar6, fVar5, fVar4, bVar3, bVar8, bVar9, bVar10, bVar11);
        fVar4 = 0.78f;
        fVar6 = _graphicUiRemapY2.fnptr!(8.0f);
        fVar6 = fVar6 + param_3;
        local_8 = fVar6;
        local_8 = _graphicUiRemapX2.fnptr!(240.0f);
        local_8 = local_8 + param_2;
        _ToMakeBtlEasyFont.fnptr!(pbVar2, local_8, fVar6, param_4, fVar4);
        if (pSVar1->owner == pSVar1->equipped_by)
        {
            bVar11 = 0x80;
            bVar10 = 0x80;
            bVar9 = 0x80;
            bVar8 = 0x80;
            bVar3 = 0x31;
            fVar4 = _graphicUiRemapY2.fnptr!(46.0f);
            fVar5 = _graphicUiRemapX2.fnptr!(38.0f);
            fVar6 = _graphicUiRemapY2.fnptr!(7.0f);
            fVar6 = fVar6 + param_3;
            fVar7 = _graphicUiRemapX2.fnptr!(180.0f);
            _DrawCrossMenuIconXYWHRGBA.fnptr!(fVar7 + param_2, fVar6, fVar5, fVar4, bVar3, bVar8, bVar9, bVar10, bVar11);
        }
        return;
    }
    // Equipment Names + Icons for Shops & Inventory
    void h_DrawCrossMenuIconWeaponName2(ushort* param_1, float param_2, float param_3, float param_4)
    {
        uint hiragana;
        byte* pbVar1;
        byte bVar2;
        float fVar3;
        float fVar4;
        float fVar5;
        float fVar6;
        ushort* ref_model_id;
        byte a;
        byte b;
        byte g;
        byte r;
        uint chr_id;

        ref_model_id = (ushort*)0x0;
        hiragana = _MsGetSaveConfigHiragana.fnptr!();
        pbVar1 = _MsWeaponName.fnptr!((int)(uint)*param_1, (int)(uint)(byte)param_1[2], (int)hiragana, ref_model_id);
        a = 0x80;
        chr_id = (uint)(byte)param_1[2];
        if (chr_id == 7)
        {
            chr_id = 37;
            bVar2 = (byte)((byte)chr_id * 2 + 2 + *(byte*)((int)param_1 + 5));
        }
        else
        {
            bVar2 = (byte)((byte)param_1[2] * 2 + 1 + *(byte*)((int)param_1 + 5));
        }
        b = 0x80;
        g = 0x80;
        r = 0x80;
        fVar3 = _graphicUiRemapY2.fnptr!(46.0f);
        fVar4 = _graphicUiRemapX2.fnptr!(38.0f);
        fVar5 = _graphicUiRemapY2.fnptr!(7.0f);
        fVar5 = fVar5 + param_3;
        fVar6 = _graphicUiRemapX2.fnptr!(180.0f);
        _DrawCrossMenuIconXYWHRGBA.fnptr!(fVar6 + param_2, fVar5, fVar4, fVar3, bVar2, r, g, b, a);
        fVar4 = 0.78f;
        fVar5 = _graphicUiRemapY2.fnptr!(8.0f);
        fVar5 = fVar5 + param_3;
        fVar3 = _graphicUiRemapX2.fnptr!(240.0f);
        _ToMakeBtlEasyFont.fnptr!(pbVar1, fVar3 + param_2, fVar5, param_4, fVar4);
        if ((byte)param_1[2] == (byte)param_1[3])
        {
            a = 0x80;
            b = 0x80;
            g = 0x80;
            r = 0x80;
            bVar2 = 0x31;
            fVar3 = _graphicUiRemapY2.fnptr!(46.0f);
            fVar4 = _graphicUiRemapX2.fnptr!(38.0f);
            fVar5 = _graphicUiRemapY2.fnptr!(7.0f);
            fVar5 = fVar5 + param_3;
            fVar6 = _graphicUiRemapX2.fnptr!(180.0f);
            _DrawCrossMenuIconXYWHRGBA.fnptr!(fVar6 + param_2, fVar5, fVar4, fVar3, bVar2, r, g, b, a);
        }
        return;
    }
    // Equipment Names + Icons for Battle Menus
    int h_TOBtlDrawCommandWindow(uint param_1)
    {
        short sVar1;
        float fVar2;
        byte bVar3;
        RGBA8 RVar4;
        int iVar5;
        int iVar6;
        byte* pbVar7;
        uint uVar8;
        Equipment* pSVar9;
        int iVar10;
        int iVar11;
        double fVar12;
        float fVar13;
        float fVar14;
        float fVar15;
        float fVar16;
        float fVar17;
        float fVar18;
        float fVar19;
        float fVar20;
        float fVar21;
        float fVar22;
        byte bVar23;
        float fVar24;
        float uVar25;
        byte bVar26;
        float fVar27;
        float uVar28;
        byte bVar29;
        RGBA8 RVar30;
        RGBA8 color_1;
        int uVar31;
        byte bVar32;
        RGBA8 color_2;
        RGBA8 color_2_00;
        RGBA8 color_2_01;
        uint uVar33;
        int local_48;
        byte* local_2c;
        Command* local_28;
        float local_24;
        float local_20;
        float local_1c;
        float local_18;
        float local_14;
        float local_10;
        float local_c;
        float local_8;
        uint colorValue;
        IntPtr functionAddress;

        local_c = 0.0f;
        fVar2 = param_1;
        fVar12 = (double)0;
        if (fVar12 < (double) * (float*)((int)param_1 + 0xdc))
        {
            fVar12 = (double)_graphicGetTime.fnptr!();
            fVar12 = fVar12 - (double) * (float*)((int)param_1 + 0xdc);
        }
        *(float*)((int)param_1 + 0xe0) = (float)fVar12;
        local_10 = *(float*)((int)param_1 + 0xe0);
        iVar11 = (int)*(short*)((int)param_1 + 0x38);
        local_28 = (Command*)((*(int*)((int)param_1 + 0x24) + -1 + iVar11) / iVar11 & 0xffff);
        if (1 < *(short*)((int)param_1 + 0x38))
        {
            iVar10 = (int)*(short*)((int)param_1 + 0x42) - (int)*(short*)((int)param_1 + 0x40);
            if ((-1 < iVar10) && (iVar10 < iVar11 * 3))
            {
                _graphicGetTime.fnptr!();
                fVar12 = MathF.Cos((float)_graphicGetTime.fnptr!());
                local_48 = (int)MathF.Round((float)(fVar12 * 32.0f + 96.0f));
                colorValue = (uint)((int)local_48 * 0x1000000 + 0x808080);
                RVar4 = new RGBA8
                {
                    a = (byte)((colorValue >> 24) & 0xFF),
                    r = (byte)((colorValue >> 16) & 0xFF),
                    g = (byte)((colorValue >> 8) & 0xFF),
                    b = (byte)(colorValue & 0xFF)
                };
                fVar27 = 156.0f;
                fVar24 = 1920.0f;
                fVar21 = 105.0f;
                fVar19 = 1619.0f;
                RVar30 = RVar4;
                fVar13 = _graphicUiRemapY2.fnptr!(50.0f);
                fVar16 = (float)(int)*(short*)((int)param_1 + 0x62);
                fVar14 = _graphicUiRemapY2.fnptr!((float)((((int)*(short*)((int)param_1 + 0x42) - (int)*(short*)((int)param_1 + 0x40)) /
                (int)*(short*)((int)param_1 + 0x38)) * 0x34) + 835.0f);
                fVar15 = _graphicUiRemapX2.fnptr!(154.0f);
                _TOMkpShapeXYWHUVC2.fnptr!(0xffffffff, fVar15 + (float)((((int)*(short*)((int)param_1 + 0x42) - (int)*(short*)((int)param_1 + 0x40)) %
                (int)*(short*)((int)param_1 + 0x38)) * (int)*(short*)((int)param_1 + 0x62)), fVar14, fVar16, fVar13, fVar19, fVar21, fVar24,
                fVar27, RVar4, RVar30);
            }
            local_8 = 0.0f;
            local_c = 0.0f;
            do
            {
                sVar1 = *(short*)((int)fVar2 + 0x62);
                fVar13 = _graphicUiRemapX2.fnptr!(154.0f);
                fVar16 = (float)(int)sVar1;
                fVar16 = (local_10 * (fVar13 + fVar16)) / 0.1f - fVar16;
                param_1 = (uint)(float)-(int)sVar1;
                local_14 = param_1;
                fVar13 = _graphicUiRemapX2.fnptr!(154.0f);
                param_1 = (uint)fVar16;
                if ((param_1 <= fVar16) && fVar13 < fVar16)
                {
                    param_1 = (uint)_graphicUiRemapX2.fnptr!(154.0f);
                }
                local_20 = (float)(int)local_c;
                color_2.r = 0x80;
                color_2.g = 0x80;
                color_2.b = 0x80;
                color_2.a = 0x60;
                RVar30.r = 0x80;
                RVar30.g = 0x80;
                RVar30.b = 0x80;
                RVar30.a = 0x60;
                local_1c = local_20 + 835.0f;
                fVar24 = 50.0f;
                fVar21 = 438.0f;
                fVar19 = 0.0f;
                fVar15 = 0.0f;
                fVar13 = _graphicUiRemapY2.fnptr!(50.0f);
                fVar16 = (float)(int)*(short*)((int)fVar2 + 0x62);
                fVar14 = _graphicUiRemapY2.fnptr!(local_1c);
                _TOMkpShapeXYWHUVC2.fnptr!(0xffffffff, param_1, fVar14, fVar16, fVar13, fVar15, fVar19, fVar21, fVar24, RVar30, color_2);
                color_2_00.r = 0x80;
                color_2_00.g = 0x80;
                color_2_00.b = 0x80;
                color_2_00.a = 0x40;
                RVar4.r = 0x80;
                RVar4.g = 0x80;
                RVar4.b = 0x80;
                RVar4.a = 0x40;
                fVar13 = (float)(int)local_8 * 53.0f + 315.0f;
                fVar24 = 1788.0f;
                fVar16 = (float)(int)local_8 * 53.0f + 270.0f;
                fVar21 = 1553.0f;
                fVar14 = _graphicUiRemapY2.fnptr!(44.0f);
                fVar15 = _graphicUiRemapX2.fnptr!(234.0f);
                fVar19 = _graphicUiRemapY2.fnptr!(local_20 + 838.0f);
                _TOMkpShapeXYWHUVC2.fnptr!(0xffffffff, param_1, fVar19, fVar15, fVar14, fVar21, fVar16, fVar24, fVar13, RVar4, color_2_00);
                sVar1 = *(short*)((int)fVar2 + 0x62);
                fVar16 = _graphicUiRemapX2.fnptr!(154.0f);
                fVar16 = ((fVar16 + (float)(sVar1 * 2)) * local_10) / 0.1f - (float)(int)sVar1;
                fVar13 = (float)-(int)sVar1;
                local_14 = fVar13;
                fVar14 = _graphicUiRemapX2.fnptr!(154.0f);
                fVar13 = fVar16;
                if ((fVar13 <= fVar16) && fVar14 + (float)(int)*(short*)((int)fVar2 + 0x62) < fVar16)
                {
                    fVar16 = _graphicUiRemapX2.fnptr!(154.0f);
                    fVar13 = fVar16 + (float)(int)*(short*)((int)fVar2 + 0x62);
                }
                color_2_01.r = 0x80;
                color_2_01.g = 0x80;
                color_2_01.b = 0x80;
                color_2_01.a = 0x60;
                color_1.r = 0x80;
                color_1.g = 0x80;
                color_1.b = 0x80;
                color_1.a = 0x60;
                fVar27 = 50.0f;
                fVar24 = 438.0f;
                fVar21 = 0.0f;
                fVar19 = 0.0f;
                fVar14 = _graphicUiRemapY2.fnptr!(50.0f);
                fVar16 = (float)(int)*(short*)((int)fVar2 + 0x62);
                fVar15 = _graphicUiRemapY2.fnptr!(local_1c);
                _TOMkpShapeXYWHUVC2.fnptr!(0xffffffff, fVar13, fVar15, fVar16, fVar14, fVar19, fVar21, fVar24, fVar27, color_1, color_2_01);
                local_10 = local_10 - 0.1f;
                local_8 = (float)((int)local_8 + 1);
                local_c = (float)((int)local_c + 0x34);
            } while ((int)local_c < 0x9c);
            fVar16 = *(float*)((int)fVar2 + 0xe0);
            if (!float.IsNaN(fVar16) && (0.3f < fVar16) != (fVar16 == 0.3f))
            {
                local_8 = 0.0f;
                local_1c = 0.0f;
                do
                {
                    fVar15 = local_1c;
                    fVar24 = (float)((int)local_8 + 1);
                    local_20 = (float)(int)fVar24;
                    uVar33 = 0x40808080;
                    uVar31 = 0x808080;
                    fVar16 = local_20 * 20.0f;
                    local_24 = (float)(int)local_8;
                    fVar13 = local_24 * 0.0f;
                    local_1c = (float)(int)local_1c;
                    local_10 = local_1c + 835.0f;
                    uVar28 = 40.0f;
                    uVar25 = 40.0f;
                    fVar22 = 3.0f;
                    fVar20 = 210.0f;
                    fVar19 = fVar13;
                    fVar21 = fVar16;
                    local_18 = fVar13;
                    fVar27 = _graphicUiRemapY2.fnptr!(3.0f);
                    fVar14 = (float)(int)*(short*)((int)fVar2 + 0x62) * 0.5f;
                    fVar17 = _graphicUiRemapY2.fnptr!(local_10);
                    fVar18 = _graphicUiRemapX2.fnptr!(154.0f);
                    _FUN_008e7d30.fnptr!(fVar18, fVar17, fVar14, fVar27, fVar19, fVar21, fVar20, fVar22, uVar25, uVar28, (uint)uVar31, uVar33);
                    fVar13 = fVar13 + 210.0f;
                    uVar33 = 0x808080;
                    uVar31 = 0x40808080;
                    uVar28 = 40.0f;
                    uVar25 = 40.0f;
                    fVar20 = 3.0f;
                    fVar18 = 210.0f;
                    fVar19 = fVar16;
                    local_14 = fVar13;
                    fVar21 = _graphicUiRemapY2.fnptr!(3.0f);
                    fVar14 = (float)(int)*(short*)((int)fVar2 + 0x62) * 0.5f;
                    fVar27 = _graphicUiRemapY2.fnptr!(local_10);
                    fVar17 = _graphicUiRemapX2.fnptr!(154.0f);
                    _FUN_008e7d30.fnptr!((float)(int)*(short*)((int)fVar2 + 0x62) * 0.5f + fVar17, fVar27, fVar14, fVar21, fVar13, fVar19, fVar18, fVar20, uVar25, uVar28, (uint)uVar31, uVar33);
                    fVar13 = local_20 * 50.0f;
                    uVar33 = 0x40808080;
                    uVar31 = 0x808080;
                    local_8 = local_1c + 880.0f;
                    uVar28 = 40.0f;
                    uVar25 = 40.0f;
                    fVar20 = 3.0f;
                    fVar18 = 210.0f;
                    fVar19 = local_18;
                    local_c = fVar13;
                    fVar21 = _graphicUiRemapY2.fnptr!(3.0f);
                    fVar14 = (float)(int)*(short*)((int)fVar2 + 0x62) * 0.5f;
                    fVar27 = _graphicUiRemapY2.fnptr!(local_8);
                    fVar17 = _graphicUiRemapX2.fnptr!(154.0f);
                    _FUN_008e7d30.fnptr!(fVar17, fVar27, fVar14, fVar21, fVar19, fVar13, fVar18, fVar20, uVar25, uVar28, (uint)uVar31, uVar33);
                    uVar33 = 0x808080;
                    uVar31 = 0x40808080;
                    uVar28 = 40.0f;
                    uVar25 = 40.0f;
                    fVar20 = 3.0f;
                    fVar18 = 210.0f;
                    fVar14 = local_14;
                    fVar19 = local_c;
                    fVar21 = _graphicUiRemapY2.fnptr!(3.0f);
                    fVar13 = (float)(int)*(short*)((int)fVar2 + 0x62) * 0.5f;
                    fVar27 = _graphicUiRemapY2.fnptr!(local_8);
                    fVar17 = _graphicUiRemapX2.fnptr!(154.0f);
                    _FUN_008e7d30.fnptr!((float)(int)*(short*)((int)fVar2 + 0x62) * 0.5f + fVar17, fVar27, fVar13, fVar21, fVar14, fVar19, fVar18, fVar20, uVar25, uVar28, (uint)uVar31, uVar33);
                    fVar13 = local_24 * 20.0f;
                    uVar33 = 0x40808080;
                    uVar31 = 0x808080;
                    uVar28 = 40.0f;
                    uVar25 = 40.0f;
                    fVar22 = 3.0f;
                    fVar20 = 210.0f;
                    fVar19 = fVar13;
                    fVar21 = fVar16;
                    local_24 = fVar13;
                    fVar27 = _graphicUiRemapY2.fnptr!(3.0f);
                    fVar14 = (float)(int)*(short*)((int)fVar2 + 0x62) * 0.5f;
                    fVar17 = _graphicUiRemapY2.fnptr!(local_10);
                    fVar18 = _graphicUiRemapX2.fnptr!(154.0f);
                    _FUN_008e7d30.fnptr!(fVar18 + (float)(int)*(short*)((int)fVar2 + 0x62), fVar17, fVar14, fVar27, fVar19, fVar21, fVar20, fVar22, uVar25, uVar28, (uint)uVar31, uVar33);
                    fVar13 = fVar13 + 210.0f;
                    uVar33 = 0x808080;
                    uVar31 = 0x40808080;
                    uVar28 = 40.0f;
                    uVar25 = 40.0f;
                    fVar18 = 3.0f;
                    fVar17 = 210.0f;
                    local_20 = fVar13;
                    fVar19 = _graphicUiRemapY2.fnptr!(3.0f);
                    fVar14 = (float)(int)*(short*)((int)fVar2 + 0x62) * 0.5f;
                    fVar21 = _graphicUiRemapY2.fnptr!(local_10);
                    fVar27 = _graphicUiRemapX2.fnptr!(154.0f);
                    _FUN_008e7d30.fnptr!((float)(int)*(short*)((int)fVar2 + 0x62) * 1.5f + fVar27, fVar21, fVar14, fVar19, fVar13, fVar16, fVar17, fVar18, uVar25, uVar28, (uint)uVar31, uVar33);
                    uVar33 = 0x40808080;
                    uVar31 = 0x808080;
                    uVar28 = 40.0f;
                    uVar25 = 40.0f;
                    fVar18 = 3.0f;
                    fVar17 = 210.0f;
                    fVar13 = local_24;
                    fVar14 = local_c;
                    fVar19 = _graphicUiRemapY2.fnptr!(3.0f);
                    fVar16 = (float)(int)*(short*)((int)fVar2 + 0x62) * 0.5f;
                    fVar21 = _graphicUiRemapY2.fnptr!(local_8);
                    fVar27 = _graphicUiRemapX2.fnptr!(154.0f);
                    _FUN_008e7d30.fnptr!(fVar27 + (float)(int)*(short*)((int)fVar2 + 0x62), fVar21, fVar16, fVar19, fVar13, fVar14, fVar17, fVar18, uVar25, uVar28, (uint)uVar31, uVar33);
                    uVar33 = 0x808080;
                    uVar31 = 0x40808080;
                    uVar28 = 40.0f;
                    uVar25 = 40.0f;
                    fVar18 = 3.0f;
                    fVar17 = 210.0f;
                    fVar13 = local_20;
                    fVar14 = local_c;
                    fVar19 = _graphicUiRemapY2.fnptr!(3.0f);
                    fVar16 = (float)(int)*(short*)((int)fVar2 + 0x62) * 0.5f;
                    fVar21 = _graphicUiRemapY2.fnptr!(local_8);
                    fVar27 = _graphicUiRemapX2.fnptr!(154.0f);
                    _FUN_008e7d30.fnptr!((float)(int)*(short*)((int)fVar2 + 0x62) * 1.5f + fVar27, fVar21, fVar16, fVar19, fVar13, fVar14, fVar17, fVar18, uVar25, uVar28, (uint)uVar31, uVar33);
                    local_1c = (float)((int)fVar15 + 0x34);
                    local_8 = fVar24;
                } while ((int)local_1c < 0x9c);
            }
        }
        iVar11 = (int)(short)local_28;
        iVar10 = (int)*(short*)((int)fVar2 + 0x3a);
        iVar5 = (int)*(short*)((int)fVar2 + 0x40) / (int)*(short*)((int)fVar2 + 0x38);
        fVar16 = _graphicUiRemapY2.fnptr!(154.0f);
        fVar13 = _graphicUiRemapX2.fnptr!(8.0f);
        fVar14 = _graphicUiRemapY2.fnptr!(835.0f);
        fVar15 = _graphicUiRemapX2.fnptr!(150.0f);
        _FUN_008e6cc0.fnptr!(fVar15 + (float)(int)*(short*)((int)fVar2 + 0x62), fVar14, fVar13, fVar16, iVar5, iVar10, iVar11);
        iVar11 = (int)(*(float*)((int)fVar2 + 0x74));
        iVar10 = (int)(*(float*)((int)fVar2 + 0x70));
        iVar5 = (int)(*(float*)((int)fVar2 + 0x6c));
        iVar6 = (int)(*(float*)((int)fVar2 + 0x68));
        _TOMakePktScissor.fnptr!(iVar6, iVar5, iVar10, iVar11);
        //(**(code**)((int)fVar2 + 0x88))(fVar2);
        functionAddress = Marshal.ReadIntPtr((IntPtr)fVar2, 0x88);
        var updateMenu = Marshal.GetDelegateForFunctionPointer<updateMenu>(functionAddress);
        updateMenu((IntPtr)fVar2);
        uVar31 = *(short*)((int)fVar2 + 0x40);
        local_20 = *(int*)((int)fVar2 + 0x24);
        local_10 = local_20;
        if ((int)uVar31 <= (int)local_20)
        {
            local_10 = (float)((int)uVar31 < 0 ? 0 : uVar31);
        }
        uVar31 = ((int)*(short*)((int)fVar2 + 0x38) * (int)*(short*)((int)fVar2 + 0x3a) + (int)local_10);
        if ((int)uVar31 <= (int)local_20)
        {
            local_20 = (float)((int)uVar31 < 0 ? 0 : uVar31);
        }
        if ((int)local_10 < (int)local_20)
        {
            do
            {
                sVar1 = *(short*)(*(int*)((int)fVar2 + 0x20) + (int)local_10 * 2);
                uVar31 = (int)_TOCheckBtlCommandUse.fnptr!((uint)(int)*(short*)((int)fVar2 + 8), (uint)(int)sVar1);
                fVar16 = local_10;
                if ((sVar1 != 0xff) && (uVar31 != -4))
                {
                    local_28 = _MsGetComData.fnptr!((uint)(int)*(short*)(*(int*)((int)fVar2 + 0x20) + (int)local_10 * 2), (int*)&local_2c);
                    sVar1 = *(short*)((int)fVar2 + 0x38);
                    pbVar7 = local_2c + local_28->name_offset;
                    uVar33 = (uint)(fVar16 == (float)(int)*(short*)((int)fVar2 + 0x42) ? 1 : 0);
                    local_8 = (float)(((int)fVar16 % (int)sVar1) * (int)*(short*)((int)fVar2 + 0x62));
                    iVar11 = (int)(*(float*)((int)fVar2 + 0x68));
                    local_14 = (float)((int)local_8 + iVar11);
                    local_8 = local_14;
                    fVar13 = _graphicUiRemapY2.fnptr!((float)((((int)fVar16 - (int)*(short*)((int)fVar2 + 0x40)) / (int)sVar1) * 0x34));
                    iVar11 = (int)(fVar13 + *(float*)((int)fVar2 + 0x7c));
                    local_18 = 0.0f;
                    if (0 < *(short*)((int)fVar2 + 0x28))
                    {
                        local_1c = (float)((int)fVar2 + 0x2c);
                        do
                        {
                            fVar13 = local_18;
                            switch (*(byte*)((int)local_1c + 1))
                            {
                                case 1:
                                    bVar32 = 0x80;
                                    bVar3 = local_28->icon;
                                    bVar29 = 0x80;
                                    bVar26 = 0x80;
                                    bVar23 = 0x80;
                                    fVar15 = _graphicUiRemapY2.fnptr!(36.0f);
                                    fVar19 = _graphicUiRemapX2.fnptr!(29.0f);
                                    fVar14 = _graphicUiRemapY2.fnptr!(3.0f);
                                    fVar14 = fVar14 + (float)iVar11;
                                    fVar21 = _graphicUiRemapX2.fnptr!(37.0f);
                                    _DrawCrossMenuIconXYWHRGBA.fnptr!(fVar21 + (float)(int)local_14, fVar14, fVar19, fVar15, bVar3, bVar23, bVar26, bVar29, bVar32);
                                    break;
                                case 2:
                                    local_24 = (float)_MsGetSaveItemNum.fnptr!((uint)(int)*(short*)(*(int*)((int)fVar2 + 0x20) + (int)fVar16 * 2));
                                    if ((0 < (int)local_24) || (*(short*)((int)fVar2 + 0x2a) == 1))
                                    {
                                        iVar10 = (int)*(short*)((int)fVar2 + 0x62) + (int)local_8;
                                        uVar8 = (uint)local_c & 0xff;
                                        uVar25 = 1.0f;
                                        fVar19 = 0.78f;
                                        fVar14 = (float)iVar11;
                                        fVar15 = _graphicUiRemapX2.fnptr!(28.0f);
                                        _FUN_009055c0.fnptr!((int)local_24, (float)iVar10 - fVar15, fVar14, (int)uVar8, fVar19, uVar25);
                                    }
                                    break;
                                case 4:
                                    local_24 = (float)_MsGetCommandMP.fnptr!((uint)(int)*(short*)((int)fVar2 + 8), (uint)local_28);
                                    if ((0 < (int)local_24) || (*(short*)((int)fVar2 + 0x2a) == 1))
                                    {
                                        iVar10 = (int)*(short*)((int)fVar2 + 0x62) + (int)local_8;
                                        uVar8 = (uint)local_c & 0xff;
                                        uVar25 = 1.0f;
                                        fVar19 = 0.78f;
                                        fVar14 = (float)iVar11;
                                        fVar15 = _graphicUiRemapX2.fnptr!(28.0f);
                                        _FUN_009055c0.fnptr!((int)local_24, (float)iVar10 - fVar15, fVar14, (int)uVar8, fVar19, uVar25);
                                    }
                                    break;
                                case 5:
                                    local_24 = (float)_MsGetRamChrHP.fnptr!((uint)(int)*(short*)(*(int*)((int)fVar2 + 0x20) + (int)fVar16 * 2));
                                    if ((0 < (int)local_24) || (*(short*)((int)fVar2 + 0x2a) == 1))
                                    {
                                        iVar10 = (int)*(short*)((int)fVar2 + 0x62) + (int)local_8;
                                        uVar8 = (uint)local_c & 0xff;
                                        uVar25 = 1.0f;
                                        fVar19 = 0.78f;
                                        fVar14 = (float)iVar11;
                                        fVar15 = _graphicUiRemapX2.fnptr!(28.0f);
                                        _FUN_009055c0.fnptr!((int)local_24, (float)iVar10 - fVar15, fVar14, (int)uVar8, fVar19, uVar25);
                                    }
                                    break;
                                case 6:
                                    local_24 = (float)_MsGetRamChrMP.fnptr!((uint)(int)*(short*)(*(int*)((int)fVar2 + 0x20) + (int)fVar16 * 2));
                                    if ((0 < (int)local_24) || (*(short*)((int)fVar2 + 0x2a) == 1))
                                    {
                                        iVar10 = (int)*(short*)((int)fVar2 + 0x62) + (int)local_8;
                                        uVar8 = (uint)local_c & 0xff;
                                        uVar25 = 1.0f;
                                        fVar19 = 0.78f;
                                        fVar14 = (float)iVar11;
                                        fVar15 = _graphicUiRemapX2.fnptr!(28.0f);
                                        _FUN_009055c0.fnptr!((int)local_24, (float)iVar10 - fVar15, fVar14, (int)uVar8, fVar19, uVar25);
                                    }
                                    break;
                                case 7:
                                    pSVar9 = _MsGetSaveWeapon.fnptr!((uint)(int)*(short*)(*(int*)((int)fVar2 + 0x20) + (int)fVar16 * 2), &local_2c);
                                    local_24 = (float)iVar11;
                                    bVar32 = 0x80;
                                    bVar29 = 0x80;
                                    bVar26 = 0x80;
                                    fVar14 = (float)(int)local_14;
                                    bVar23 = 0x80;
                                    if (pSVar9->owner == 7)
                                    {
                                        bVar3 = (byte)(37 * 2 + 2 + pSVar9->type);
                                    }
                                    else
                                    {
                                        bVar3 = (byte)(pSVar9->owner * 2 + 1 + pSVar9->type);
                                    }
                                    fVar15 = _graphicUiRemapY2.fnptr!(36.0f);
                                    fVar19 = _graphicUiRemapX2.fnptr!(29.0f);
                                    fVar13 = _graphicUiRemapY2.fnptr!(2.0f);
                                    fVar13 = fVar13 + local_24;
                                    fVar21 = _graphicUiRemapX2.fnptr!(37.0f);
                                    _DrawCrossMenuIconXYWHRGBA.fnptr!(fVar21 + fVar14, fVar13, fVar19, fVar15, bVar3, bVar23, bVar26, bVar29, bVar32);
                                    fVar13 = local_18;
                                    if (pSVar9->owner == pSVar9->equipped_by)
                                    {
                                        bVar32 = 0x80;
                                        bVar29 = 0x80;
                                        bVar26 = 0x80;
                                        bVar23 = 0x80;
                                        bVar3 = 0x31;
                                        fVar15 = _graphicUiRemapY2.fnptr!(36.0f);
                                        fVar19 = _graphicUiRemapX2.fnptr!(29.0f);
                                        fVar13 = _graphicUiRemapY2.fnptr!(2.0f);
                                        fVar13 = fVar13 + local_24;
                                        fVar21 = _graphicUiRemapX2.fnptr!(37.0f);
                                        _DrawCrossMenuIconXYWHRGBA.fnptr!(fVar21 + fVar14, fVar13, fVar19, fVar15, bVar3, bVar23, bVar26, bVar29, bVar32);
                                        fVar13 = local_18;
                                    }
                                    break;
                                case 0x10:
                                    local_c = (uVar31 < 0 ? 1.0f : 0.0f);
                                    iVar10 = 0;
                                    if (*(int*)((int)fVar2 + 0xe4) == 0)
                                    {
                                        _FUN_00904ba0.fnptr!((byte*)pbVar7, (float)((float)(int)*(short*)((int)fVar2 + 0x62) * 0.5 + (float)(int)local_14), (float)iVar11,
                                        (float)(int)*(short*)((int)fVar2 + 0x66), (byte)(uVar31 < 0 ? 1 : 0), 0.78f, (uint)1.0f, 1, (int)uVar33, 0);
                                    }
                                    else
                                    {
                                        iVar5 = 0;
                                        uVar25 = 1.0f;
                                        fVar24 = 0.78f;
                                        fVar14 = (float)(int)*(short*)((int)fVar2 + 0x66);
                                        fVar15 = (float)iVar11;
                                        fVar19 = local_c;
                                        uVar8 = uVar33;
                                        fVar21 = _graphicUiRemapX2.fnptr!(72.0f);
                                        _FUN_00904ba0.fnptr!((byte*)pbVar7, fVar21 + (float)(int)local_14, fVar15, fVar14, (byte)fVar19, fVar24, (uint)uVar25, iVar5, (int)uVar8,
                                        iVar10);
                                    }
                                    break;
                            }
                            local_1c = (float)((int)local_1c + 2);
                            local_18 = (float)((int)fVar13 + 1);
                        } while ((int)local_18 < (int)*(short*)((int)fVar2 + 0x28));
                    }
                }
                local_10 = (float)((int)local_10 + 1);
            } while ((int)local_10 < (int)local_20);
        }
        return 0;
    }
    // HP/MP Copying for Equip Menu
    void h_FUN_008cf800(int param_1)
    {
        bool bVar1;
        int uVar2;
        uint uVar4;
        int iVar3;

        *(int*)(param_1 + 0x1c) = 0;
        *DAT_0186a5ec = 0;
        *DAT_0186a5f0 = 0xff;
        *TKMenuFaceRatio = 0;
        *TkMenuFaceKeep = (int)_FUN_008a9820.fnptr!();
        *TkMenuFaceNew = (int)_FUN_008a9820.fnptr!();
        *TkMenuFaceOld = (int)_FUN_008a9820.fnptr!();
        uVar4 = 0;
        do
        {
            uVar2 = _FUN_008a97d0.fnptr!(uVar4);
            (DAT_0186a634)[uVar4] = uVar2;
            uVar2 = _FUN_008a9c20.fnptr!(uVar4);
            (DAT_0186a614)[uVar4] = uVar2;
            uVar2 = (int)_FUN_008a9870.fnptr!(uVar4);
            (DAT_0186a654)[uVar4] = uVar2;
            uVar2 = (int)_FUN_008a9920.fnptr!(uVar4);
            (DAT_0186a674)[uVar4] = uVar2;
            uVar4 = uVar4 + 1;
        } while ((int)uVar4 < 8);
        *DAT_0186a5e4 = 0;
        bVar1 = _FUN_008cfc00.fnptr!();
        iVar3 = bVar1 ? 1 : 0;
        uVar2 = _TkMenuGetCurrentPlayer.fnptr!();
        _FUN_008cfcf0.fnptr!(uVar2, iVar3);
        *DAT_0186a5d8 = 0x155;
        *DAT_0186a5d4 = 0;
        _FUN_008c2bd0.fnptr!(scene0String);
        _FUN_008c2bd0.fnptr!(scene11String);
        _FUN_008c2bd0.fnptr!(scene20String);
        return;
    }
    // Battle Results: Equipment Drops
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

        iVar4 = _FUN_00798be0.fnptr!((BtlRewardData*)param_3);
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
            bVar1 = _MsGetSavePlyJoined.fnptr!((byte)iVar7);
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
        uVar5 = _Brnd.fnptr!(0xc);
        iVar10 = 0;
        iVar7 = 0;
        do
        {
            if (_MsGetSavePlyJoined.fnptr!((byte)iVar7) != 0)
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
        uVar5 = _Brnd.fnptr!(0xc);
        *(byte*)(iVar4 + 0x103 + param_3) = (byte)((byte)uVar5 & 1);
        *(byte*)(iVar4 + 0x104 + param_3) = 0xff;
        *(byte*)(iVar4 + 0x106 + param_3) = *(byte*)(param_2 + 0x2e);
        *(byte*)(iVar4 + 0x107 + param_3) = *(byte*)(param_2 + 0x30);
        *(byte*)(iVar4 + 0x108 + param_3) = *(byte*)(param_2 + 0x2f);
        uVar5 = _Brnd.fnptr!(0xc);
        uVar6 = _Brnd.fnptr!(0xc);
        iVar7 = (int)(*(byte*)(param_2 + 0x2d) + ((uVar5 & 7) - 4));
        iVar7 = _MsCheckRange.fnptr!((int)(iVar7 + (iVar7 >> 0x1f & 3U)) >> 2, 1, 4);
        *(byte*)(iVar4 + 0x109 + param_3) = (byte)iVar7;
        iVar9 = (int)(*(byte*)(param_2 + 0x31) + ((uVar6 & 7) - 4));
        gearOwner = bVar1;
        if (gearOwner == 7)
        {
            gearOwner = 3; // Checks if the gear rng rolled belongs to Seymour,
                           // and assigns the gear's auto-abilities to another character's field.
                           // (here, Seymour gets Kimahri's auto-abilities)
        }
        puVar11 = (ushort*)((byte*)param_2 + 0x32 + (*(byte*)(iVar4 + 0x103 + param_3) + gearOwner * 2) * 0x10);
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
                uVar5 = _Brnd.fnptr!(0xd);
                uVar3 = puVar11[(int)uVar5 % 7 + 1];
                if (uVar3 != 0)
                {
                    uVar5 = _FUN_00798aa0.fnptr!(uVar3);
                    iVar7 = 0;
                    if (0 < iVar4)
                    {
                        puVar8 = &gear->abilities[0];
                        do
                        {
                            uVar6 = _FUN_00798aa0.fnptr!(*puVar8);
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
        uVar3 = _MsWeaponNameNum.fnptr!(gear);
        gear->name_id = uVar3;
        _MsWeaponName.fnptr!((int)(uint)uVar3, (int)(uint)gear->owner, 0, &gear->model_id);
        return 0;
    }
    // Show Gear in Menus
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
                    pSVar2 = _MsGetSaveWeapon.fnptr!(gear, (byte**)0x0);
                    pSVar2->flags = (byte)(pSVar2->flags ^ (param_2 * '\x02' ^ pSVar2->flags) & 2);
                }
                loop = loop + 1;
            } while (loop < 2);
        }
        return;
    }
    // Gear Ability Preview in Shops
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
        pvVar1 = _TkMn2GetExcelData.fnptr!(*p_DAT_0186aadc_curShopIdx, (ExcelDataFile*)*(nint*)p_DAT_0186ab68_arms_shop_bin_ptr);
        if (param_2 == 0)
        {
            pSVar2 = _MsGetSaveWeapon.fnptr!(p_DAT_01597730_OvrModesMenuList[*(short*)(param_1 + 0x48)].overdrive_id, &local_10);
        }
        else
        {
            pSVar2 = (Equipment*)_FUN_008d9140.fnptr!(*(ushort*)((int)pvVar1 + *(short*)(param_1 + 0x48) * 2 + 2));
        }
        if (pSVar2->type == 0)
        {
            gear_inv_idx = _FUN_008a9c20.fnptr!(DAT_0186ab60);
        }
        else
        {
            gear_inv_idx = _FUN_008a97d0.fnptr!(DAT_0186ab60);
        }
        if (gear_inv_idx == 0xff)
        {
            pSVar3 = (Equipment*)0x0;
        }
        else
        {
            pSVar3 = _MsGetSaveWeapon.fnptr!(gear_inv_idx, &local_14);
        }
        iVar12 = 2;
        fVar5 = _graphicUiRemapY2.fnptr!((float)60.0);
        fVar6 = _graphicUiRemapX2.fnptr!((float)740.0);
        fVar7 = _graphicUiRemapY2.fnptr!((float)295.0);
        fVar8 = _graphicUiRemapX2.fnptr!((float)1144.0);
        _TODrawMenuPlateXYWHType.fnptr!(fVar8, fVar7, fVar6, fVar5, iVar12);
        fVar5 = _graphicUiRemapY2.fnptr!((float)36.0);
        fVar6 = _graphicUiRemapX2.fnptr!((float)430.0);
        fVar7 = _graphicUiRemapY2.fnptr!((float)310.0);
        fVar8 = _graphicUiRemapX2.fnptr!((float)1299.0);
        _FUN_008f8bb0.fnptr!(0x12, fVar8, fVar7, fVar6, fVar5);
        if (param_2 == 0)
        {
            if (pSVar3 == (Equipment*)0x0)
            {
                iVar12 = 9;
                fVar5 = _graphicUiRemapY2.fnptr!((float)64.0);
                fVar6 = _graphicUiRemapX2.fnptr!((float)700.0);
                fVar7 = _graphicUiRemapY2.fnptr!((float)231.0);
                fVar8 = _graphicUiRemapX2.fnptr!((float)1164.0);
                _TODrawMenuPlateXYWHType.fnptr!(fVar8, fVar7, fVar6, fVar5, iVar12);
                pfVar9 = &local_c;
                scale = 0x3f47ae14;
                uVar11 = 0;
                pbVar4 = (byte*)_FUN_008bee40.fnptr!(0x17);
                _ToGetBtlEasyFontWidth.fnptr!(pbVar4, pfVar9, uVar11, scale);
                fVar7 = (float)0.78;
                pSVar3 = (Equipment*)0x0;
                fVar6 = _graphicUiRemapY2.fnptr!((float)243.0);
                fVar5 = _graphicUiRemapX2.fnptr!((float)1514.0);
                fVar5 = (float)(fVar5 - local_c * 0.5);
                local_8 = fVar5;
                goto LAB_008d8976;
            }
        }
        else if (pSVar3 == (Equipment*)0x0)
        {
            iVar12 = 9;
            fVar5 = _graphicUiRemapY2.fnptr!((float)64.0);
            fVar6 = _graphicUiRemapX2.fnptr!((float)700.0);
            fVar7 = _graphicUiRemapY2.fnptr!((float)231.0);
            fVar8 = _graphicUiRemapX2.fnptr!((float)1164.0);
            _TODrawMenuPlateXYWHType.fnptr!(fVar8, fVar7, fVar6, fVar5, iVar12);
            pfVar9 = &local_8;
            uVar11 = 0x3f47ae14;
            pSVar10 = pSVar3;
            pbVar4 = (byte*)_FUN_008bee40.fnptr!(0x17);
            _ToGetBtlEasyFontWidth.fnptr!(pbVar4, pfVar9, (int)pSVar10, uVar11);
            fVar7 = (float)0.78;
            fVar6 = _graphicUiRemapY2.fnptr!((float)243.0);
            fVar5 = _graphicUiRemapX2.fnptr!((float)1514.0);
            fVar5 = (float)(fVar5 - local_8 * 0.5);
            local_c = fVar5;
            goto LAB_008d8976;
        }
        pSVar10 = pSVar3;
        fVar5 = _graphicUiRemapY2.fnptr!((float)363.0);
        fVar6 = _graphicUiRemapX2.fnptr!((float)1144.0);
        _FUN_008d8a70.fnptr!(fVar6, fVar5, (int)pSVar10);
        iVar12 = 9;
        fVar5 = _graphicUiRemapY2.fnptr!((float)64.0);
        fVar6 = _graphicUiRemapX2.fnptr!((float)700.0);
        fVar7 = _graphicUiRemapY2.fnptr!((float)231.0);
        fVar8 = _graphicUiRemapX2.fnptr!((float)1164.0);
        _TODrawMenuPlateXYWHType.fnptr!(fVar8, fVar7, fVar6, fVar5, iVar12);
        fVar7 = (float)0.0;
        fVar5 = _graphicUiRemapY2.fnptr!((float)231.0);
        fVar6 = _graphicUiRemapX2.fnptr!((float)1164.0);
        _DrawCrossMenuIconWeaponName2.fnptr!(&pSVar3->name_id, fVar6, fVar5, fVar7);
    LAB_008d8976:
        pbVar4 = (byte*)_FUN_008bee40.fnptr!(0x17);
        _ToMakeBtlEasyFont.fnptr!(pbVar4, fVar5, fVar6, 0.0f, fVar7);
        goto LAB_008d898c;
    LAB_008d898c:
        iVar12 = 2;
        fVar5 = _graphicUiRemapY2.fnptr!((float)60.0);
        fVar6 = _graphicUiRemapX2.fnptr!((float)740.0);
        fVar7 = _graphicUiRemapY2.fnptr!((float)659.0);
        fVar8 = _graphicUiRemapX2.fnptr!((float)970.0);
        _TODrawMenuPlateXYWHType.fnptr!(fVar8, fVar7, fVar6, fVar5, iVar12);
        fVar5 = _graphicUiRemapY2.fnptr!((float)36.0);
        fVar6 = _graphicUiRemapX2.fnptr!((float)430.0);
        fVar7 = _graphicUiRemapY2.fnptr!((float)671.0);
        fVar8 = _graphicUiRemapX2.fnptr!((float)1125.0);
        _FUN_008f8bb0.fnptr!(0x13, fVar8, fVar7, fVar6, fVar5);
        fVar5 = _graphicUiRemapY2.fnptr!((float)727.0);
        fVar6 = _graphicUiRemapX2.fnptr!((float)970.0);
        _FUN_008d8a70.fnptr!(fVar6, fVar5, (int)pSVar2);
        return;
    }

    // Overdrive Mode Learning
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
        uVar1 = _MsGetRamChrMonster.fnptr!(param_1);
        uVar2 = _MsGetRamChrMonster.fnptr!(param_3);
        if ((((*(byte*)(param_4 + 0x5bb) == 0x13) && (-1 < param_5)) && (uVar1 == 1)) && (uVar2 == 0)) // Aeons Only
        {
            _MsLimitUp.fnptr!((int)param_3, (Chr*)param_4, (uint)(param_6 * 0x12) / *(uint*)(param_4 + 0x594) + 1);
        }
        if (param_5 < 1)
        {
            if (((param_5 < 0) && (uVar1 == 0)) && ((uVar2 == 0 && (param_1 != param_3))))
            {
                iVar4 = 1;
                _FUN_007b10d0.fnptr!(param_1, 0x03, 0);
                if (*(byte*)(param_2 + 0x5bb) == 0x03) // Healer
                {
                    iVar3 = (int)(*(uint*)(param_4 + 0x594) - *(int*)(param_4 + 0x5d0));
                    iVar5 = -param_5;
                    if (-iVar3 != param_5 && iVar3 <= -param_5)
                    {
                        iVar5 = iVar3;
                    }
                    _MsLimitUp.fnptr!((int)param_1, (Chr*)param_2, (uint)(iVar5 << 4) / *(uint*)(param_4 + 0x594) + 1);
                }
            }
        }
        else if (uVar1 == 1)
        {
            if (uVar2 == 0)
            {
                _FUN_007b10d0.fnptr!(param_3, 0x02, 0);
                if (*(byte*)(param_4 + 0x5bb) == 0x02) // Stoic
                {
                    _MsLimitUp.fnptr!((int)param_3, (Chr*)param_4, (uint)(param_5 * 0x1e) / *(uint*)(param_4 + 0x594) + 1);
                }
                uVar1 = 0;
                param_5 = 1;
                do
                {
                    character = _MsGetChr.fnptr!(uVar1);
                    if ((character->in_battle != 0) && (uVar1 != param_3))
                    {
                        param_5 = param_5 + 1;
                        _FUN_007b10d0.fnptr!(uVar1, 0x01, 0);
                        if (character->ram.limit_mode_selected == 0x01) // Comrade
                        {
                            _MsLimitUp.fnptr!((int)uVar1, character, (uint)(iVar5 * 0x14) / *(uint*)(param_4 + 0x594) + 1);
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
            _FUN_007b10d0.fnptr!(param_1, 0x00, 0);
            if (*(byte*)(param_2 + 0x5bb) == 0x00) // Warrior
            {
                uVar1 = (uint)((param_5 * 10) / *(int*)(param_2 + 0x6f4) + 1);
                if (0x10 < (int)uVar1)
                {
                    uVar1 = 0x10;
                }
                _MsLimitUp.fnptr!((int)param_1, (Chr*)param_2, uVar1);
            }
            if (*(byte*)(param_2 + 0x5bb) == 0x13) // Aeons Only
            {
                _MsLimitUp.fnptr!((int)param_1, (Chr*)param_2, (uint)(((param_5 << 4) / *(int*)(param_2 + 0x6f4)) / 10 + 1));
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
        uVar1 = (int)_MsGetRamChrMonster.fnptr!((uint)param_1);
        uVar2 = _MsGetRamChrMonster.fnptr!(param_3);
        if (uVar1 == 1)
        {
            if (uVar2 == 0)
            {
                uVar1 = 0;
                do
                {
                    character = _MsGetChr.fnptr!((uint)uVar1);
                    if ((character->in_battle != 0) && (uVar1 != param_3))
                    {
                        iVar3 = iVar3 + 1;
                        _FUN_007b10d0.fnptr!((uint)uVar1, 0x07, 0);
                        if (character->ram.limit_mode_selected == 0x07) // Avenger
                        {
                            _MsLimitUp.fnptr!(uVar1, character, 0x1e);
                        }
                    }
                    uVar1 = uVar1 + 1;
                } while (uVar1 < 8);
                return iVar3;
            }
        }
        else if ((uVar1 == 0) && (uVar2 == 1))
        {
            _FUN_007b10d0.fnptr!((uint)param_1, 0x08, 0);
            if (*(byte*)(param_2 + 0x5bb) == 0x08) // Slayer
            {
                _MsLimitUp.fnptr!(param_1, (Chr*)param_2, 0x14);
            }
            if ((*(int*)(param_2 + 0x6f4) * 0x14 < *(int*)(param_4 + 0x594)) || (9999 < *(int*)(param_4 + 0x594)))
            {
                _FUN_007b10d0.fnptr!((uint)param_1, 0x09, 0);
            }
            if ((*(byte*)(param_2 + 0x5bb) == 0x09) && ((uint)(*(int*)(param_2 + 0x6f4) * 3) < *(uint*)(param_4 + 0x594))) // Hero
            {
                _MsLimitUp.fnptr!(param_1, (Chr*)param_2, 0x14);
            }
        }
        return 0;
    }
    int h_FUN_007b10d0(uint chr_id, uint limit_mode, int param_3)
    {
        Chr* chr = _MsGetChr.fnptr!(chr_id);

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
        _FUN_007b10d0.fnptr!(param_1, 0x0D, 0);
        if (*(byte*)(param_2 + 0x5bb) == 0x0D) // Ally
        {
            _MsLimitUp.fnptr!((int)param_1, (Chr*)param_2, 3);
        }
        iVar2 = _MsCalcWeakLevel.fnptr!(*(int*)(param_2 + 0x5d0), *(int*)(param_2 + 0x594));
        if (0 < iVar2)
        {
            local_8 = 2;
            _FUN_007b10d0.fnptr!(param_1, 0x0F, 0);
            if (*(byte*)(param_2 + 0x5bb) == 0x0F) // Daredevil
            {
                _MsLimitUp.fnptr!((int)param_1, (Chr*)param_2, 5);
            }
        }
        uVar3 = 0;
        param_1 = 0;
        chr_id = 0;
        do
        {
            character = _MsGetChr.fnptr!(chr_id);
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
            _FUN_007b10d0.fnptr!(uVar1, 0x10, 0);
            if (*(byte*)(param_2 + 0x5bb) == 0x10) // Loner
            {
                _MsLimitUp.fnptr!((int)uVar1, (Chr*)param_2, 0x10);
            }
        }
        if (((((*(ushort*)(param_2 + 0x606) & 10) == 0) &&
           ((*(ushort*)(param_2 + 0x606) & 0x100) == 0)) &&
           ((*(byte*)(param_2 + 0x608) == 0 &&
           (((*(byte*)(param_2 + 0x609) == 0 &&
           (*(byte*)(param_2 + 0x60a) == 0)) &&
           (*(byte*)(param_2 + 0x614) == 0)))))) &&
           ((*(ushort*)(param_2 + 0x616) & 0x4000) == 0))
        {
            return local_8;
        }
        _FUN_007b10d0.fnptr!(uVar1, 0x0E, 0);
        if (*(byte*)(param_2 + 0x5bb) == 0x0E) // Sufferer
        {
            _MsLimitUp.fnptr!((int)uVar1, (Chr*)param_2, 0x10);
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
            character = _MsGetChr.fnptr!((uint)chr_id);
            if (character->in_battle != 0)
            {
                iVar1 = iVar1 + 1;
                _FUN_007b10d0.fnptr!((uint)chr_id, 0x0B, 0);
                if (character->ram.limit_mode_selected == 0x0B) // Victor
                {
                    _MsLimitUp.fnptr!(chr_id, character, 0x14);
                }
            }
            chr_id = chr_id + 1;
        } while (chr_id < 8);
        return iVar1;
    }

    void h_MsSetSaveStartGame()
    {
        _MsSetSaveStartGame.chain_from(h_MsSetSaveStartGame).fnptr!();

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
                    gear->name_id = _MsWeaponNameNum.fnptr!(gear);
                }
                else
                {
                    gear->slot_count = 2;
                    gear->abilities[1] = 0x8000;
                    gear->name_id = _MsWeaponNameNum.fnptr!(gear);
                }
                _MsWeaponName.fnptr!(gear->name_id, gear->owner, 0, &gear->model_id);
            }
        }

        PlySave* seymour = _MsGetSavePlayerPtr.fnptr!(7);
        seymour->base_mp = 319;
        seymour->mp = 319;
        seymour->max_mp = 319;
        seymour->base_defense = 17;
        seymour->base_magic = 32;
        seymour->base_magic_defense = 40;
        seymour->base_agility = 15;
        seymour->base_evasion = 3;
        seymour->slv_spent = 30;
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
        _MsSetSaveParam.fnptr!(7);

        Command* requiem = _MsGetRomPlyCommand.fnptr!(0x30E3, (int*)0x0);
        requiem->is_piercing = true;
        requiem->flags_damage = 4; // Can Crit
        requiem->dmg_formula = 15; // Special MAG
        requiem->power = 45;

        Command* extra24 = _MsGetRomPlyCommand.fnptr!(0x3130, (int*)0x0);
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

    // Show Seymour's Armor Model
    int h_FUN_00635c20(uint param_1)
    {
        short sVar1;

        sVar1 = (short)_getScenerioFlag.fnptr!();
        if (param_1 != 0x4068)
        {
            if (sVar1 == 0x2e)
            {
                if (param_1 != 5 && param_1 != 6 && param_1 != 0x109b && param_1 != 0x5001 && param_1 != 0x5002)
                {
                    return (int)(param_1 & 0xffffff00);
                }
            }
            else
            {
                param_1 &= 0xfffff000;
                if (g_eventId != 0x17e || (int)param_1 > 0x3fff)
                {
                    return (int)(param_1 & 0xffffff00);
                }
            }
        }
        return (int)((param_1 & 0xffffff00) | 0x01);
    }

    // Summoning
    // Extra24 = Seymour Summon
    int h_MsParseCommand(byte* param_1)
    {
        uint uVar13 = param_1[2];
        int iVar14 = (int)uVar13 * 0x10;
        ushort* com_id = (ushort*)(param_1 + iVar14 + 8);

        if (*com_id == 0x3130)
        {
            *com_id = 0x3117;
            int result = _MsParseCommand.chain_from(h_MsParseCommand).fnptr!(param_1);
            *com_id = 0x3130;
            return result;
        }
        return _MsParseCommand.chain_from(h_MsParseCommand).fnptr!(param_1);
    }

    // Extra24 Summon Help text
    void h_TOBtlCtrlHelpWin(int param_1)
    {
        int window_id = *toBwNum;
        BtlWindow* currentwindow = &Globals.Battle.windows[window_id];

        if (currentwindow->window_command_id == 0x3130)
        {
            currentwindow->window_command_id = 0x3117;
            _TOBtlCtrlHelpWin.chain_from(h_TOBtlCtrlHelpWin).fnptr!(param_1);
            currentwindow->window_command_id = 0x3130;
            return;
        }
        _TOBtlCtrlHelpWin.chain_from(h_TOBtlCtrlHelpWin).fnptr!(param_1);
    }

    // Battle Summon List
    ushort* h_TOGetSaveWindow(uint chr_id, BtlWindowType window_type, int* summonlistlength)
    {
        if ((uint)window_type == 5)
        {
            ushort* originallist = _TOGetSaveWindow.chain_from(h_TOGetSaveWindow).fnptr!(chr_id, window_type, summonlistlength);
            Span<ushort> listSpan = new(originallist, *summonlistlength);
            if (chr_id == 1)
            {
                if (!Globals.save_data->has_anima && listSpan.Contains<ushort>(PlySaveId.PC_ANIMA))
                {
                    int newLength = 0;
                    for (int i = 0; i < *summonlistlength; i++)
                    {
                        if (listSpan[i] != PlySaveId.PC_ANIMA)
                        {
                            listSpan[newLength] = listSpan[i];
                            newLength++;
                        }
                    }
                    for (int i = newLength; i < *summonlistlength; i++)
                    {
                        listSpan[i] = 0xFFFF;
                    }
                    *summonlistlength = newLength;
                }
                return originallist;
            }
            if (chr_id == 7)
            {
                if (listSpan.Contains<ushort>(PlySaveId.PC_ANIMA))
                {
                    listSpan.Fill(0xFFFF);
                    listSpan[0] = PlySaveId.PC_ANIMA;
                    *summonlistlength = 1;
                    return originallist;
                }
                else
                {
                    listSpan.Fill(0xFFFF);
                    *summonlistlength = 0;
                    return originallist;
                }
            }
            else
            {
                return originallist;
            }
        }
        return _TOGetSaveWindow.chain_from(h_TOGetSaveWindow).fnptr!(chr_id, window_type, summonlistlength);
    }

    // Pause Menu -> Overdrive Menu
    int h_TkMenuSummonEnableMask()
    {
        if (_TkMenuGetCurrentPlayer.fnptr!() == 1)
        {
            if (!Globals.save_data->has_anima)
            {
                return (int)(_TkMenuSummonEnableMask.chain_from(h_TkMenuSummonEnableMask).fnptr!() & ~(1u << 0x0D)); // Display Anima in Yuna's Overdrive menu once unlocked
            }
        }
        return _TkMenuSummonEnableMask.chain_from(h_TkMenuSummonEnableMask).fnptr!();
    }

    // Anima Stat Scaling with Seymour
    void h_MsSetSaveParam(uint chr_id)
    {
        aeon = chr_id;
        _MsSetSaveParam.chain_from(h_MsSetSaveParam).fnptr!(chr_id);
        aeon = 0;
    }

    int* h_FUN_00785c20(uint chr_id, uint* param_2)
    {
        MsChrAbilityMap* pMVar1;

        if (chr_id == 1 && aeon == 0x0D)
        {
            chr_id = 7; // Scale with Seymour
        }
        pMVar1 = _MsGetChrAbilityMap.fnptr!(chr_id);
        param_2[8] = Globals.save_data->ply_saves[(int)chr_id].base_hp;
        param_2[9] = Globals.save_data->ply_saves[(int)chr_id].base_mp;
        if (chr_id == 7 && aeon == 0x0D)
        {
            *param_2 = 0;
            param_2[1] = 0;
            // Removed Strength & Defense scalings - Anima was too overpowered with them
            // when using Seymour's stats as a base.
        }
        else
        {
            *param_2 = Globals.save_data->ply_saves[(int)chr_id].base_strength;
            param_2[1] = Globals.save_data->ply_saves[(int)chr_id].base_defense;
        }
        param_2[2] = Globals.save_data->ply_saves[(int)chr_id].base_magic;
        param_2[3] = Globals.save_data->ply_saves[(int)chr_id].base_magic_defense;
        param_2[4] = Globals.save_data->ply_saves[(int)chr_id].base_agility;
        param_2[5] = Globals.save_data->ply_saves[(int)chr_id].base_luck;
        param_2[6] = Globals.save_data->ply_saves[(int)chr_id].base_evasion;
        param_2[7] = Globals.save_data->ply_saves[(int)chr_id].base_accuracy;
        if (pMVar1 != (MsChrAbilityMap*)0x0)
        {
            param_2[8] = (uint)(param_2[8] + pMVar1->hp * 0x32);
            param_2[9] = (uint)(param_2[9] + pMVar1->mp * 5);
            *param_2 = *param_2 + pMVar1->strength;
            param_2[1] = param_2[1] + pMVar1->defense;
            param_2[2] = param_2[2] + pMVar1->magic;
            param_2[3] = param_2[3] + pMVar1->magic_defense;
            param_2[4] = param_2[4] + pMVar1->agility;
            param_2[5] = param_2[5] + pMVar1->luck;
            param_2[6] = param_2[6] + pMVar1->evasion;
            param_2[7] = param_2[7] + pMVar1->accuracy;
        }
        return (int*)pMVar1;
    }

    // Prevent Seymour from Softlocking by Disabling Known Bugged Abilities
    void h_MsBtlReadManage()
    {
        int old_state = Globals.Battle.btl->battle_state;

        _MsBtlReadManage.chain_from(h_MsBtlReadManage).fnptr!();

        if (Globals.Battle.btl->battle_state != 13 || old_state == Globals.Battle.btl->battle_state) return;

        // Post Battle Start
        if (Globals.Battle.player_characters == null) return;

        _setCommandDisabled.fnptr!(PlySaveId.PC_SEYMOUR, PlayerCommandId.PCOM_USE, 1);
        _setCommandDisabled.fnptr!(PlySaveId.PC_SEYMOUR, PlayerCommandId.PCOM_SPARE_CHANGE, 1);
        _setCommandDisabled.fnptr!(PlySaveId.PC_SEYMOUR, PlayerCommandId.PCOM_THREATEN, 1);
        _setCommandDisabled.fnptr!(PlySaveId.PC_SEYMOUR, PlayerCommandId.PCOM_PROVOKE, 1);
        _setCommandDisabled.fnptr!(PlySaveId.PC_SEYMOUR, PlayerCommandId.PCOM_BRIBE, 1);
    }
}
