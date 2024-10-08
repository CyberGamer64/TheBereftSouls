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

			}
		}
		// Thorium Blood is a decorative block so create a recipe to convert standard blood back into Thorium blood.
		public override void AddRecipes()
		{
			if (ModLoader.TryGetMod("ThoriumMod", out Mod ThoriumMod))
			{
				Recipe.Create(ThoriumMod.Find<ModItem>("Blood"))
				{
					.AddIngredient<Items.Materials.StandardBlood>(3);
				}
			}
		}
	}
}
