using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using TheBereftSouls.Common.Utility;
using TheBereftSouls.Items.Materials;

namespace TheBereftSouls.Common.Global
{
	public class RecipeOverride : ModSystem
	{
		private int ingredientAmount;

		// NOTE or TO DO: This method of overwriting Recipes checks for each mod seperately but it isn't weak referenced. 


		//Untested - JIT used to prevent crashes if mods aren't enabled.

		[JITWhenModsEnabled("CalamityMod","ThoriumMod")]
		public override void PostAddRecipes()
		{


			// Checks to see if Calamity is installed before making changes.
			if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod)){
				// Replace Calamity Blood Orb in Recipes with Standardised Blood.
				foreach (Recipe recipe in Main.recipe){
					if (recipe.TryGetIngredient(CalamityMod.Find<ModItem>("BloodOrb"), out Item ingredient))
					{
						ingredientAmount = ingredient.stack;
						recipe.RemoveIngredient(ingredient);
						recipe.AddIngredient<Items.Materials.StandardBlood>(ingredientAmount);
					}
				}
				// Add Recipes in case of mod being enabled later.
                Recipe bloodRecipe = Recipe.Create(ModContent.ItemType<Items.Materials.StandardBlood>(), 1);
                bloodRecipe.AddIngredient(CalamityMod.Find<ModItem>("BloodOrb"));
                bloodRecipe.Register();
            }
			// Checks to see if Thorium is installed before making changes.
			if (ModLoader.TryGetMod("ThoriumMod", out Mod ThoriumMod))
			{
				// Replace Thorium Blood in Recipes with Standardised Blood.
				foreach (Recipe recipe in Main.recipe)
				{
					if (recipe.TryGetIngredient(ThoriumMod.Find<ModItem>("Blood"), out Item ingredient))
					{
						ingredientAmount = ingredient.stack;
						recipe.RemoveIngredient(ingredient);
						recipe.AddIngredient<Items.Materials.StandardBlood>(ingredientAmount);
					}
				}
				// Add Recipes in case of mod being enabled later.
				// Thorium Blood is also placeable so allow reconversion.
                var resultItem = ThoriumMod.Find<ModItem>("Blood");
                resultItem.CreateRecipe()

                    .AddIngredient<Items.Materials.StandardBlood>(5)

                    .Register();
                // Convert Thorium Blood to Standard.
                Recipe bloodRecipe = Recipe.Create(ModContent.ItemType<Items.Materials.StandardBlood>(), 1);
                bloodRecipe.AddIngredient(ThoriumMod.Find<ModItem>("Blood"));
                bloodRecipe.Register();
            }
		}
	}
}
