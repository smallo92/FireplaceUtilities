# Fireplace Utilities
This mod adds a whole bunch of customisation options and new features for fireplaces

## Manual Installation
To install this mod, you need to have BepInEx. After installing BepInEx, extract FireplaceUtilities.dll into games install **"\Valheim\BepInEx\plugins"**

## Config
Before the config file is generated, you must run the game with the mod installed. The config file is located at **"\Valheim\BepInEx\config\smallo.mods.fireplaceutilities.cfg"**

#### There are serveral config options available;

## Main Toggles;
| Config Option | Type | Default Value | Description |
|:-------------:|:-----------:|:-----------:|:-----------|
| Enable Mod | bool | true | Enable or disable the mod |
| Burn Items In Fire | bool | true | Allows you to burn items in fires |
| Extinguish Fires | bool | true | Allows you to turn fires off/on |
| Disable Fires During The Day | bool | false | Allows you to make fires turn off during the day, you must press a key on each item to let it toggle |
| Return Fuel | bool | true | Allows you to press a key to return the fuel left in a fire |
| Torch and Sconce Use Coal | bool | true | Makes the Wood/Iron Torch and Sconce use Coal as fuel instead of resin |
| Custom Burn Times | bool | false | Enable custom burn times for all fireplaces, the default values are the games vanilla values |

## Burn Items In Fire Options;
| Config Option | Type | Default Value | Description |
|:-------------:|:-----------:|:-----------:|:-----------|
| Burn In Torches | bool | false | Allows items to be burnt in ground torches, wall torches or braziers |
| Give Coal | bool | true | Returns coal when burning an item |
| Blacklist Items | string | $item_wood | Items that aren't allowed to be burned. Seperate items by a comma. Wood should remain as a default so that way it doesn't take your wood twice when lighting a fire, if you have a mod that allows other wood types to burn, put them on this list. |
| Burn Item Text | string | Burn item | The text to show when hovering over the fire |
| Coal Amount | int | 1 | Amount of coal to give when burning an item |
| Burn Key | string | LeftShift | The key to use in combination with the hotkeys. KeyCodes can be found [here](https://docs.unity3d.com/ScriptReference/KeyCode.html) |
| Burn Key Text | string | LShift | The custom text to show for the string, if you set it to "none" then it'll use what you have in the 'Key' config option. |

## Extinguish Fires Options;
| Config Option | Type | Default Value | Description |
|:-------------:|:-----------:|:-----------:|:-----------|
| Extinguish Fire Text | string | Extinguish fire | The text to show when hovering over the fire |
| Ignite Fire Text | string | Ignite fire | The text to show when hovering over the fire if the fire is extinguished |
| Put Out Fire Key | string | LeftAlt | The key to use to put out a fire. KeyCodes can be found [here](https://docs.unity3d.com/ScriptReference/KeyCode.html) |
| Put Out Fire Key Text | string | LAlt | The custom text to show for the string, if you set it to "none" then it'll use what you have in the 'Key' config option. |

## Return Fuel Options;
| Config Option | Type | Default Value | Description |
|:-------------:|:-----------:|:-----------:|:-----------|
| Return Fuel Text | string | Return fuel | The text to show when hovering over the fire |
| Return Fuel Key | string | LeftControl | The key to use to put out a fire. KeyCodes can be found [here](https://docs.unity3d.com/ScriptReference/KeyCode.html) |
| Return Fuel Key Text | string | LCtrl | The custom text to show for the string, if you set it to "none" then it'll use what you have in the 'Key' config option. 

## Disable Fires During The Day Options;
| Config Option | Type | Default Value | Description |
|:-------------:|:-----------:|:-----------:|:-----------|
| Time Toggle On Text | string | Enable Timer | The text to show when hovering over the fire to enable the timer |
| Time Toggle Off Text | string | Disable Timer | The text to show when hovering over the fire to disable the timer |
| Time Toggle Key | string | Equals | The key to use to put out a fire. KeyCodes can be found [here](https://docs.unity3d.com/ScriptReference/KeyCode.html) |
| Time Toggle Key Text | string | = | The custom text to show for the string, if you set it to "none" then it'll use what you have in the 'Key' config option. 

## Custom Burn Times Options;
| Config Option | Type | Default Value | Description |
|:-------------:|:-----------:|:-----------:|:-----------|
| Firepit | int | 5000 | Custom burntime for the standard firepit |
| Wood Ground Torch | int | 10000 | Custom burntime for the wooden ground torch |
| Bonfire | int | 5000 | Custom burntime for the bonfire |
| Hearth | int | 5000 | Custom burntime for the hearth |
| Sconce | int | 20000 | Custom burntime for the sconce |
| Iron Ground Torch | int | 20000 | Custom burntime for the iron ground torch |
| Green Ground Torch | int | 20000 | Custom burntime for the green ground torch |
| Brazier | int | 20000 | Custom burntime for the brazier |

If you have any suggestions, feel free to let me know!

## Changelog

#### v2.1.0;
* Removed the custom time defintion for turning fireplaces off, they now turn on at 6am and turn off at 6pm which is when the new day and you are getting cold splashscreens appear
* Fixed the fireplaces maxing out when you put in 5 items

#### v2.0.1;
* Config update (oops)

#### v2.0.0;
* Ability to define times to turn on/off fires
* Ability to extinguish and ignite fires without losing ANY fuel
* Ability to get unused fuel back from the fireplaces
* Ability to define custom burn times for all fireplace types

#### v1.0.2;
* Additional error checking for getting fireplaces

#### v1.0.1;
* Readme fix

#### v1.0.0;
* Initial release

## Images

![Showing](https://fivem.fail/gta5/Misc/N_0xa1183bcfee0f93d1/sVuCbLoPwp.png)

[Discord Support](https://discord.gg/pTGSu8R7DW)

## Affiliate Link
[![ZAP-Hosting Gameserver and Webhosting](https://zap-hosting.com/interface/download/images.php?type=affiliate&id=99496)](https://zap-hosting.com/a/f386564816225e9bcd3445ae47b34c8823f72489)