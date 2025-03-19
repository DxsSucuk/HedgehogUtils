using RoR2;
using System;
using System.Text;
using UnityEngine;
using R2API;

namespace HedgehogUtils
{
    public static class Language
    {
        //maybe go back to what the mod had by default with the separate lang file
        public static void Initialize()
        {
            #region Super Form
            string superSonicColor = "<color=#ffee00>";
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "SUPER_FORM_PREFIX", "Super {0}");

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "SUPER_FORM", "Super");

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "SUPER_FORM_ANNOUNCE_TEXT", superSonicColor + "<size=110%>{0} has transformed into their {1} form!</color></size>");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "SUPER_FORM_ANNOUNCE_TEXT_2P", superSonicColor + "<size=110%>You transformed into your {1} form!</color></size>");

            #endregion

            #region Emeralds
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "EMERALD_TEMPLE_NAME", "Chaos Emerald");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "EMERALD_TEMPLE_CONTEXT", "Receive Emerald");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "EMERALD_TEMPLE_INSPECT", "When activated by a survivor the Chaos Emerald will be dropped. Once all seven are collected, survivors can transform into their Super form.");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "EMERALD_TEMPLE_TITLE", "Chaos Emerald");

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "EMERALD_TEMPLE_YELLOW", "Chaos Emerald: Yellow");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "EMERALD_TEMPLE_BLUE", "Chaos Emerald: Blue");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "EMERALD_TEMPLE_RED", "Chaos Emerald: Red");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "EMERALD_TEMPLE_GRAY", "Chaos Emerald: Gray");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "EMERALD_TEMPLE_GREEN", "Chaos Emerald: Green");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "EMERALD_TEMPLE_CYAN", "Chaos Emerald: Cyan");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "EMERALD_TEMPLE_PURPLE", "Chaos Emerald: Purple");
            // Couldn't figure out how to change the tokens at runtime to match the keybind config but ehhhhh whatever
            string chaosEmeraldDesc = $" of the <style=cIsUtility>seven</style> Chaos Emeralds." + Environment.NewLine + $"When all <style=cIsUtility>seven</style> are collected by you and/or other players, press {superSonicColor}V</color> to transform into your {superSonicColor}Super form</color> for {superSonicColor}{Forms.SuperForm.StaticValues.superSonicDuration}</color> seconds. Transforming increases <style=cIsDamage>damage</style> by <style=cIsDamage>+{100f * Forms.SuperForm.StaticValues.superSonicBaseDamage}%</style>. Increases <style=cIsDamage>attack speed</style> by <style=cIsDamage>+{100f * Forms.SuperForm.StaticValues.superSonicAttackSpeed}%</style>. Increases <style=cIsUtility>movement speed</style> by <style=cIsUtility>+{100f * Forms.SuperForm.StaticValues.superSonicMovementSpeed}%</style>. Grants <style=cIsHealing>complete invincibility</style> and <style=cIsUtility>flight</style>. For <style=cIsUtility>Sonic</style>, {superSonicColor}all of his skills are upgraded</color>." + Environment.NewLine + Environment.NewLine + "This will <style=cIsUtility>consume</style> all seven Chaos Emeralds.";
            string chaosEmeraldPickup = $"One out of seven. When all are collected, transform into your Super form by pressing V, granting invincibility, flight, and incredible power for {Forms.SuperForm.StaticValues.superSonicDuration} seconds. Consumed on use.";

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "YELLOW_EMERALD", "Chaos Emerald: <style=cIsDamage>Yellow</style>");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "YELLOW_EMERALD_PICKUP", chaosEmeraldPickup);
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "YELLOW_EMERALD_DESC", $"<style=cIsDamage>One</style>" + chaosEmeraldDesc);

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "BLUE_EMERALD", "Chaos Emerald: <color=#2b44d6>Blue</color>");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "BLUE_EMERALD_PICKUP", chaosEmeraldPickup);
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "BLUE_EMERALD_DESC", $"<color=#2b44d6>One</color>" + chaosEmeraldDesc);

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "RED_EMERALD", "Chaos Emerald: <style=cDeath>Red</style>");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "RED_EMERALD_PICKUP", chaosEmeraldPickup);
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "RED_EMERALD_DESC", $"<style=cDeath>One</style>" + chaosEmeraldDesc);

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "GRAY_EMERALD", "Chaos Emerald: <color=#b8c5d6>Gray</color>");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "GRAY_EMERALD_PICKUP", chaosEmeraldPickup);
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "GRAY_EMERALD_DESC", "<color=#b8c5d6>One</color>" + chaosEmeraldDesc);

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "GREEN_EMERALD", "Chaos Emerald: <style=cIsHealing>Green</style>");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "GREEN_EMERALD_PICKUP", chaosEmeraldPickup);
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "GREEN_EMERALD_DESC", $"<style=cIsHealing>One</style>" + chaosEmeraldDesc);

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "CYAN_EMERALD", "Chaos Emerald: <style=cIsUtility>Cyan</style>");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "CYAN_EMERALD_PICKUP", chaosEmeraldPickup);
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "CYAN_EMERALD_DESC", $"<style=cIsUtility>One</style>" + chaosEmeraldDesc);

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "PURPLE_EMERALD", "Chaos Emerald: <color=#c437c0>Purple</color>");
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "PURPLE_EMERALD_PICKUP", chaosEmeraldPickup);
            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "PURPLE_EMERALD_DESC", "<color=#c437c0>One</color>" + chaosEmeraldDesc);

            #region Emerald Lore

            string dataScraperOpening = "<style=cMono>Welcome to DataScraper (v3.1.53 – beta branch)\n$ Accessing Cyber-Space...\n$ Scraping memory... error.\n$ Resolving...\n$";

            string dataScraperEnding = "\n$ Combing for relevant data... done.\nDisplaying partial result.</style>\n\n";

            string translationError = "\n\n<style=cMono>Translation Errors:</style>\n";
            StringBuilder sb = new StringBuilder();

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "BLUE_EMERALD_LORE", dataScraperOpening + FileNotFoundEmeraldLogHelper(sb, 0) + dataScraperEnding +
                "Only a fraction of us were able to make it off world before it attacked. It was as if death itself had claimed our homeworld, leaving nothing but smoldering rock where our home planet once was.\n\nThe emeralds powered our engines. It was only with their power that any of us managed to escape.\n\nAll we could do then was move forward into the darkness with only the glimmering light of the emeralds to guide us.");

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "CYAN_EMERALD_LORE", dataScraperOpening + FileNotFoundEmeraldLogHelper(sb, 1) + dataScraperEnding +
                "There's far more to these gems than we know about. It couldn't have just been random chance that drew us to this world. The emeralds reacted to something.. no.. something took control of the emeralds, and by extension, our ships. Whatever it is, it's connected to the emeralds in some way. In the end, it doesn't really matter why it brought us here anyway. I had long since gotten used to the chaos.\n\nThe world that strange force had brought us to was a primitive one, many millenia behind us. We chose to isolate ourselves on an uninhabited archipelago to avoid interfering too much with this world's inhabitants. With our numbers so slim, these islands had plenty of room for us. Besides, we are no conquerors.\n\nNo one should have their home taken away from them.");

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "GRAY_EMERALD_LORE", dataScraperOpening + FileNotFoundEmeraldLogHelper(sb, 2) + dataScraperEnding +
                "---. In cyber space we kept our history, our memories, our hopes, our souls. In the digital dream, it felt as if the home we had lost was still with us.");

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "GREEN_EMERALD_LORE", dataScraperOpening + FileNotFoundEmeraldLogHelper(sb, 3) + dataScraperEnding +
                "Before, we had run away and lost almost everything. Now, not only was what little we had left in danger once more, we had also dragged a planet that's not our own into this conflict.\n\nWe could've run again, we could've tried to hide on another world, we could've left this world to die like ours.\n\nHow much would we lose in our rushed and desperate escape? Was there any guarantee it wouldn't find us again? How many more worlds would be in danger from this... thing?\n\nWe could've run away.\n\nWe didn't.");

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "PURPLE_EMERALD_LORE", dataScraperOpening + FileNotFoundEmeraldLogHelper(sb, 4) + dataScraperEnding +
                "[The End] had followed us and brought with it the threat of destroying another world full of life. The Chaos Emeralds powered our newest weapons in this last fight against it. Even with our finest technology fueled by the incredible power of the emeralds, we could not match its overwhelming power. As a last resort, we sealed it within cyber space.\n\n[The End] is far more terrifying than I imagined. It's intelligent. It's.. clever. It can speak our language. It knows who we are on a personal level, as if it has witnessed our past.\n\nHow does a being, with no purpose other than indiscriminately destroying all, know so much? What worries me even more is why it knows all this.\n\nEven managing to subdue its destructive power by trapping it in cyber space, [The End] cannot be underestimated. Its ability to understand others will make it dangerously manipulative."+translationError+"# [The End] could not be fully translated.");

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "RED_EMERALD_LORE", dataScraperOpening + FileNotFoundEmeraldLogHelper(sb, 5) + dataScraperEnding +
                "It took our home world. It took our lives. All that remains of us are our memories, stored in cyber space. I have long since passed away. These ramblings of mine are data stored in cyber space, too. It was made to keep the memories and teachings of our people from being forgotten. How ironic.\n\nA design as grand as cyber space was made to make sure our knowledge would be preserved and remembered, and yet I hope it is forgotten. I hope no one finds us, lest they release the very thing that reduced our civilization to this sad state.\n\nAfter everything thats happened, life moves on, with or without us. We had no place in this world to begin with.\n\nEven though we'll be long gone with time, I know the emeralds will still remain. They were our greatest treasure. They will be our gift to this planet. Its brilliant shine shall pierce through the chaos.");

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "YELLOW_EMERALD_LORE", dataScraperOpening + FileNotFoundEmeraldLogHelper(sb, 6) + dataScraperEnding +
                "If anyone is reading this, know that I wish for nothing but your safety. There's much I'd like to share with you, but this space isn't safe. Any interaction with cyber space risks releasing [The End] again. If you find anything of use from within our ruins, it is yours to take. Perhaps it can save you from suffering a fate like ours.\n\nAll that remains of me now is the memory of my failure replaying over and over. My failure to protect my friends and family in the face of [The End]. Maybe being able to save you, whoever you may be, will be enough for me to move on." + translationError + "# [The End] could not be fully translated.");

            #endregion

            #endregion

            LanguageAPI.Add(HedgehogUtilsPlugin.Prefix + "LAUNCH_KEYWORD",
                "<style=CKeywordName>Launch</style><style=cSub>Turn the hit enemy into a projectile that <style=cIsUtility>flys in the direction hit</style> and <style=cIsDamage>deals damage</style> to other enemies it hits equal to the damage that launched it.");
        }
        internal static string FileNotFoundEmeraldLogHelper(StringBuilder sb, int index)
        {
            sb.Clear();
            int count = 0;
            while (count < 7)
            {
                if (count == index)
                {
                    sb.Append(" [ ]");
                }
                else
                {
                    sb.Append(" [X]");
                }
                count++;
            }
            return sb.ToString();
        }
    }
}