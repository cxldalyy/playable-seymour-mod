This mod features modded files to be used in conjunction with [Fahrenheit](https://github.com/fahrenheit-crew/fahrenheit)'s External File Loader (EFL) to improve Seymour's integration and stability in the Final Fantasy X HD Remaster.

### You must own or obtain a legal copy of Final Fantasy X/X-2 HD Remaster.

### This mod must be played for the first time on a brand new save file. Loading a pre-existing save will break countless things, and Seymour may be unplayable.

### Prerequisites:
- [Fahrenheit](https://github.com/fahrenheit-crew/fahrenheit) v1.0.0-alpha10

### Installation:
For the sake of conserving disk space, extract only the following files from the `FFX_Data` folder:
```
menu/abmap/dat00/D3D11/lines
menu/abmap/dat12/D3D11/lines
menu/D3D11/face_ply
menu_us/D3D11/icon
```
- Extract your original, unmodified `ffx_ps2` folder from `FFX_Data.vbf` using the [VBF Browser](https://www.nexusmods.com/finalfantasy12/mods/3).
- Place the extracted `FFX_Data` and `ffx_ps2` folders into the `\original` folder within this release.
- Run "apply_mods.bat".
- This will convert the .xdelta patches in `\patches` into the modded files ready for EFL use.
- Copy the new modded files in `efl\x` into the `mods\playable-seymour\efl\x` folder inside the release. You can now also delete the whole `efl files` folder.
- Finally, copy the entire `mods` folder from the release into your Fahrenheit folder in the game directory.
- Done! Now launch the game through Fahrenheit.
- Once the installation is completed, you can also delete the `FFX_Data` and `ffx_ps2` folders from the `\original` folder to conserve disk space.
- Enjoy the story of Spira with Seymour permanently by your side!

### Features
This mod makes Seymour his own, standalone, 8th party member. This includes giving him his own Equipment, Sphere Grid (though not his own path), Overdrives, Stats etc. He can use both black and white magic, and follows Lulu's path on the Sphere Grid by default. With this in mind, his base stats are slightly adjusted compared to the vanilla game, so he and Lulu can both be powerful black mages respectively. As a result of starting on Lulu's grid though, his white magic is unfortunately limited to the few spells he starts with, though they can still be handy when the party loses Yuna, or in a pinch!

### Summoning
- Seymour comes with the ability to summon Anima (and only Anima) once recruited immediately after the Sinspawn Gui battle (though she's not able to be summoned for that particular battle). Seymour requires a full Overdrive to summon, giving the player the choice between summoning, or using Requiem. This means that if you really don't like that Seymour can summon, you don't have to summon!
- Given this choice, Requiem and Anima have both been adjusted and rebalanced, with Requiem now being far more powerful, and Anima's stats being slightly less overpowered early on (given that she joins the party much earlier), making it a genuine strategic decision to choose between the two for an Overdrive.
- This does mean that Seymour can now summon Anima where she would otherwise not be able to appear, most notably against Crawler, Seymour, Wendigo, Home, Evrae and Bevelle. As such, she may be buggy in these areas - particularly the Overdrive camera angling. She is fully functional though - and shouldn't cause any issues in these areas outside of that.
- Despite Seymour being able to summon Anima, Yuna retains vanilla behaviour and cannot summon her until the Baaj Temple sidequest is completed. If the player doesn't complete it, Seymour remains the only character able to summon her. Once the sidequest is cleared, both Yuna and Seymour can summon Anima as usual.
- Anima now scales with Seymour's stats instead of Yuna's. Be sure to level him on the Sphere Grid if you want Anima to grow in strength. Yuna and the other Aeons remain untouched, and scale as usual according to her stats.

### Additional Improvements:
Optional, but recommended additions that improve quality of life:
- My [Requiem Fix](https://www.nexusmods.com/finalfantasyxx2hdremaster/mods/266) to fix Seymour's Overdrive's camera angling.

### Final Notes:
This mod is still a work in progress. There are still a few details that need addressing, like equipment sorting and softlocking abilities. The mod in its current state is however entirely playable, start to finish, with no game-breaking issues that I know of. Of course, if you do find any bugs, please raise an issue here.

### Made possible thanks to:
- **[VBF Browser](https://www.nexusmods.com/finalfantasy12/mods/3):** Topher, ffgriever and Vaan
- **[Fahrenheit](https://github.com/fahrenheit-crew/fahrenheit):** The Fahrenheit Crew
- **[custom-character](https://github.com/Rurusachi/custom-character):** Rurusachi, for the Sphere Grid work included in this mod to feature Seymour on the grid.
- **[xDelta3](https://github.com/jmacd/xdelta-gpl):** Joshua MacDonald

This mod would not have been possible without all of these incredible people listed above and their hard work. All credit goes to them.
