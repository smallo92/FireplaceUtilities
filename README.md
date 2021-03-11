# Fireplace Utilities
This mod adds a few things for fireplaces. Being able to burn items in a fireplace to dispose of them and get coal in return. Being able to disable a fire and get the fuel items back and finally making wood/iron torches/sconces use coal instead of resin as a fuel source.

## Manual Installation
To install this mod, you need to have BepInEx. After installing BepInEx, extract FireplaceUtilities.dll into games install **"\Valheim\BepInEx\plugins"**

## Config
Before the config file is generated, you must run the game with the mod installed. The config file is located at **"\Valheim\BepInEx\config\smallo.mods.fireplaceutilities.cfg"**

#### There are serveral config options available;
| Config Option | Type | Default Value | Description |
|:-------------:|:-----------:|:-----------:|:-----------|
| Enable Mod | bool | true | Enable or disable the mod |
| Burn Items In Fire | bool | true | Allows you to burn items in fires |
| Extinguish Fires | bool | true | Allows you to turn fires off |
| Torch and Sconce Use Coal | bool | true | Makes the Wood/Iron Torch and Sconce use Coal as fuel instead of coal |
| Burn In Torches | bool | false | Allows items to be burnt in ground torches, wall torches or braziers |
| Give Coal | bool | true | Returns coal when burning an item |
| Blacklist Items | string | $item_wood | Items that aren't allowed to be burned. Seperate items by a comma. Wood should remain as a default so that way it doesn't take your wood twice when lighting a fire, if you have a mod that allows other wood types to burn, put them on this list. |
| Burn Item Text | string | Burn item | The text to show when hovering over the fire |
| Coal Amount | int | 1 | Amount of coal to give when burning an item |
| Burn Key | string | LeftShift | The key to use in combination with the hotkeys. KeyCodes can be found [here](https://docs.unity3d.com/ScriptReference/KeyCode.html) |
| Burn Key Text | string | LShift | The custom text to show for the string, if you set it to "none" then it'll use what you have in the 'Key' config option. |
| Give Back Fuel | bool | true | Returns the remaining fuel left back. Which is 1 less then the amount in the fire, since technically 1 is currently being burnt. |
| Extinguish Fire Text | string | Extinguish fire | The text to show when hovering over the fire |
| Put Out Fire Key | string | LeftAlt | The key to use to put out a fire. KeyCodes can be found [here](https://docs.unity3d.com/ScriptReference/KeyCode.html) |
| Put Out Fire Key Text | string | LAlt | The custom text to show for the string, if you set it to "none" then it'll use what you have in the 'Key' config option. |

If you have any suggestions, feel free to let me know!

## Changelog

#### v1.0.1;
* Readme fix

#### v1.0.0;
* Initial release

## Images

![Showing](https://fivem.fail/gta5/Misc/N_0xa1183bcfee0f93d1/sVuCbLoPwp.png)