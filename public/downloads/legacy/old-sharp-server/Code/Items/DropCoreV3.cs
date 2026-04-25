/*
 * Created by STeeL
 * 2013
 * Please read file Readme.txt before use this source file
 * 
 * (C) STeeL-Team 2013
 */
using System;
using System.IO;
using System.Linq;
using System.Collections;
using RhynnServer.Code.Core;
using RhynnServer.Code.AI;
using RhynnServer.Code.Network;

namespace RhynnServer.Code.Items
{
	/// <summary>
	/// Description of DropCoreV3.
	/// </summary>
	public class DropCoreV3
	{
		public static Random srand=null;
		public static void BeginDrop(Mob who,int level,ClientObject attacker,bool Legendary)
		{
			try
			{
				if(srand == null)
				{
					srand = new Random();
				}
				int LegendaryChance = level/8;
				int Plevel = Cache.CacheStorage.GetCharById(attacker.SelectedCharID).level;
				if(Legendary)
				{
					LegendaryChance+=20;
				}
				
				int itemChance = 60;
				int goldChance = 50;//80%
				int potionChance = 45;//80%
				int scrollChance = 20;
				
				bool IsLegendary = ChanceCalculator.Calculate(LegendaryChance);
				bool IsItem = ChanceCalculator.Calculate(itemChance);
				bool IsGold = ChanceCalculator.Calculate(goldChance);
				bool IsPotion = ChanceCalculator.Calculate(potionChance);
				bool IsScroll = ChanceCalculator.Calculate(scrollChance);
				
				if(IsItem)
				{
					int rar = 5;
					if(IsLegendary)
					{
						rar = 10;
					}
					DropItem(who,level,rar);
				}
				if(IsGold)
				{
					DropGold(who,int.Parse(XmlReader.GetValueFromXml("Data/Mobs/"+who.MobID.ToString()+".xml","/Mob/DropGOLD")));
				}
				if(IsPotion)
				{
					DropPotion(who);
				}
				if(IsScroll)
				{
					DropScroll(who);
				}
			}
			catch(Exception ex)
			{
				ServConsole.Debug(ex.ToString());
			}
		}
		
		private static int Swap(int type)
		{
			//1 - оружие,2 - щит,3 - нагрудник,4 - шлем,5 - боты,6 - перчи
			//items types:
			// 1 - helm
			// 2 - armor
			// 3 - weapon
			// 4 - shield
			// 5 - glove
			// 6 - boot
			if(type == 1)
			{
				return 3;
			}
			else if(type == 2)
			{
				return 4;
			}
			else if(type == 3)
			{
				return 2;
			}
			else if(type == 4)
			{
				return 1;
			}
			else if(type == 5)
			{
				return 6;
			}
			else if(type == 6)
			{
				return 5;
			}
			return 0;
		}
		
		public static string[] GenerateWeaponName(int wtype)
		{
			string[] result = new string[2];
			//0 - арбалеты,1 - дубинки,2 - звездочки,3 - копья
            //4 - луки,5 - мечи,6 - ножи,7 - палки,8 - посохи,9 - топоры
            int check = 0;
			switch(wtype)
			{
				case 0:
					result[0] = "Knight crossbow";
					result[1] = "Weak crossbow";
					break;
				case 1:
					check = srand.Next(0,6);
					
					if(check == 0)
					{
						result[0] = "Club";
						result[1] = "Weak Mace";
					}
					else if(check == 1)
					{
						result[0] = "Morning Star";
						result[1] = "Quality Mace";
					}
					else if(check == 2)
					{
						result[0] = "Scull Mace";
						result[1] = "Better Mace";
					}
					else if(check == 3)
					{
						result[0] = "Spiked Club";
						result[1] = "Normal Mace";
					}
					else if(check == 4)
					{
						result[0] = "Flail";
						result[1] = "Fine Mace";
					}
					else if(check == 5)
					{
						result[0] = "Orc Club";
						result[1] = "Valuable Mace";
					}
					else if(check == 6)
					{
						result[0] = "Strong Club";
						result[1] = "Normal Mace";
					}
					break;
				case 2:
					result[0] = "Throwing stars";
					result[1] = "Weak stars";
					break;
				case 3:
					check = srand.Next(0,1);
					
					if(check == 0)
					{
						result[0] = "Heavy spear";
						result[1] = "Weak spear";
					}
					else
					{
						result[0] = "Light spear";
						result[1] = "Weak spear";
					}
					break;
				case 4:
					check = srand.Next(0,3);
					
					if(check == 0)
					{
						result[0] = "Short Bow";
						result[1] = "Simple Bow";
					}
					else if(check == 1)
					{
						result[0] = "Bow";
						result[1] = "Common Bow";
					}
					else if(check == 2)
					{
						result[0] = "Rain Bow";
						result[1] = "Weird Bow";
					}
					else if(check == 3)
					{
						result[0] = "Heavy Bow";
						result[1] = "Better Bow";
					}
					break;
				case 5:
					check = srand.Next(0,5);
					
					if(check == 0)
					{
						result[0] = "Sharp Blade";
						result[1] = "Fine Sword";
					}
					else if(check == 1)
					{
						result[0] = "Heavy sword";
						result[1] = "Weak sword";
					}
					else if(check == 2)
					{
						result[0] = "Pirate sword";
		   				result[1] = "Weak sword";
					}
					else if(check == 3)
					{
						result[0] = "Crystal sword";
					   	result[1] = "Weak sword";
					}
					else if(check == 4)
					{
						result[0] = "Ice sword";
						result[1] = "Weak sword";
					}
					else if(check == 5)
					{
						result[0] = "Elf Blade";
						result[1] = "Weak Sword";
					}
					break;
				case 6:
					check = srand.Next(0,2);
					
					if(check == 0)
					{
						result[0] = "Knife";
						result[1] = "Common Knife";
					}
					else if(check == 1)
					{
						result[0] = "Sharp Knife";
						result[1] = "Enhanced Knife";
					}
					else if(check == 2)
					{
						result[0] = "Wolf Claw";
						result[1] = "Better Knife";
					}
					
					break;
				case 7:
					check = srand.Next(0,3);
					
					if(check == 0)
					{
						result[0] = "Dead Staff";
						result[1] = "Magic Staff";
					}
					else if(check == 1)
					{
						result[0] = "Gold staff";
						result[1] ="Weak staff";
					}
					else if(check == 2)
					{
						result[0] = "Light staff";
						result[1] = "Weak staff";
					}
					else if(check == 3)
					{
						result[0] = "Heavy spear";
						result[1] = "Weak spear";
					}

					break;
				case 8:
					check = srand.Next(0,2);
					
					if(check == 0)
					{
						result[0] = "Mage Scepter";
						result[1] ="Magic Scepter";
					}
					else if(check == 1)
					{
						result[0] = "Ancient Scepter";
						result[1] = "Superior Scepter";
					}
					else if(check == 2)
					{
						result[0] = "Power scepter";
						result[1] = "Weak scepter";
					}
					break;
				case 9:
					check = srand.Next(0,3);
					
					if(check == 0)
					{
						result[0] = "Dwarf Axe";
						result[1] = "Exeptional Axe";
					}
					else if(check == 1)
					{
						result[0] = "Heavy Axe";
						result[1] = "Weak axe";
					}
					else if(check == 2)
					{
						result[0] = "Trees axe";
						result[1] = "Weak axe";
					}
					else if(check == 3)
					{
						result[0] = "Dark Axe";
						result[1] = "Weak Axe";
					}
					break;
			}
			return result;
		}
			
		
		public static void DropItem(Mob who,int level,int rarity)
		{
			Random rand=new Random();
						
			bool legendary = false;
			if(rarity == 10)
			{
				legendary = true;
			}
				
			int gold = 0; //стоимость шмотки
			int lv1_stat_atck = 2;//атака
			int lv1_stat_dmg = 1;//дамаг
			int lv1_stat_def = 2;//защита
			int lv1_stat_heal = 3;//хп
			int lv1_stat_mana = 2;//мана
			int lv1_stat_skill = 0;//скилл
			int lv1_stat_magic = 0;//магия
			int hregen = 0;
			int mregen = 0;
			int reqSkill = 0;
			int reqMagic = 0;
			
			int frequency = 120;
			int range = 7;
			
			
			int rndCof = 0;
			
			int GraphicsID = 100006;
			int GraphicsX  = 0;
			int GraphicsY  = 0;
			string selectedName = "Thing";
			string selectedDescr = "Just thing...";
			
			
			int generatedId = rand.Next(100000,999999);				
			//generate item
			//1 - оружие,2 - щит,3 - нагрудник,4 - шлем,5 - боты,6 - перчи
			int type = rand.Next(1,6);
			/* here we should generate item */
			Hashtable result = item_drop(Swap(type),level,rarity);
			reqSkill = (int)result["rskill"];
			reqMagic = (int)result["rmagic"];
			lv1_stat_heal = (int)result["hp"];
			lv1_stat_mana = (int)result["mana"];
			lv1_stat_dmg = (int)result["dmg"];
			lv1_stat_atck = (int)result["atk"];
			lv1_stat_def = (int)result["def"];
			lv1_stat_skill = (int)result["skill"];
			lv1_stat_magic = (int)result["magic"];
			hregen = (int)result["hregen"];
			mregen = (int)result["mregen"];
			
			
			/*
 			  * next lines of code maked only for test!!!
			  * in release version we replace it with template system
			  * so it's add ability to do items look closer to 1.3.x version
			  * of rhynn
			  * <begining temp code>
			*/
			//graphics id generator
			switch(type)
			{
				case 1:
					//дальнее,или нет?
					bool rangeWeapon = false;//T - значит дальнее :)
					if(Core.ChanceCalculator.Calculate(50))
					{
						rangeWeapon = true;						
					}
					if(rangeWeapon)
					{						
						rndCof = rand.Next(1,24);
						range = 64 + rndCof;
						int gen = rand.Next(1,5);
						switch(gen)
						{
							case 1:
								{
								//лук
								string[] data = GenerateWeaponName(4);
								selectedName = data[0];
								selectedDescr = data[1];
								GraphicsX = 30;
								GraphicsY = 45;
								}
								break;
							case 2:
								{
								//лук
								string[] data = GenerateWeaponName(4);
								selectedName = data[0];
								selectedDescr = data[1];
								GraphicsX = 30;
								GraphicsY = 60;
								}
								break;
							case 3:
								{
								//лук
								string[] data = GenerateWeaponName(4);
								selectedName = data[0];
								selectedDescr = data[1];
								GraphicsX = 30;
								GraphicsY = 75;
								}
								break;
							case 4:
								{
								//арбалет
								string[] data = GenerateWeaponName(0);
								selectedName = data[0];
								selectedDescr = data[1];
								GraphicsX = 30;
								GraphicsY = 90;
								}
								break;
							case 5:
								{
								//звезда
								string[] data = GenerateWeaponName(2);
								selectedName = data[0];
								selectedDescr = data[1];
								GraphicsX = 30;
								GraphicsY = 105;
								}
								break;
						}
					}
					else
					{
						rndCof = rand.Next(1,level/2);
						frequency +=rndCof;
						int gen = rand.Next(1,20);
						switch(gen)
						{
																//0 - арбалеты,1 - дубинки,2 - звездочки,3 - копья
            //4 - луки,5 - мечи,6 - ножи,7 - палки,8 - посохи,9 - топоры
							case 1:
            					{
								//меч
								string[] data = GenerateWeaponName(5);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 0;
								GraphicsY = 90;
            					}
								break;
							case 2:
								{
								//посох
								string[] data = GenerateWeaponName(8);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 0;
								GraphicsY = 45;
								}
								break;
							case 3:
								//меч
								{
								string[] data = GenerateWeaponName(5);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 0;
								GraphicsY = 105;
								}
								break;
							case 4:
								//дубинка
								{
								string[] data = GenerateWeaponName(1);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 0;
								GraphicsY = 30;
								}
								break;
							case 5:
								//дубинка
								{
								string[] data = GenerateWeaponName(1);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 0;
								GraphicsY = 15;
								}
								break;
							case 6:
								{
								//меч
								string[] data = GenerateWeaponName(5);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 15;
								GraphicsY = 0;
								}
								break;
							case 7:
								{
								//топор
								string[] data = GenerateWeaponName(9);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 15;
								GraphicsY = 75;
								}
								break;
							case 8:
								{
								//дубинка
								string[] data = GenerateWeaponName(1);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 0;
								GraphicsY = 0;
								}
								break;
							case 9:
								{
								//нож
								string[] data = GenerateWeaponName(6);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 0;
								GraphicsY = 60;
								}
								break;
							case 10:
								{
								//нож
								string[] data = GenerateWeaponName(6);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 0;
								GraphicsY = 75;
								}
								break;
							case 11:
								{
								//топор
								string[] data = GenerateWeaponName(9);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 15;
								GraphicsY = 60;
								}
								break;
							case 12:
								{
								//топор
								string[] data = GenerateWeaponName(9);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 15;
								GraphicsY = 90;
								}
								break;
							case 13:
								{
								//посох
								string[] data = GenerateWeaponName(8);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 15;
								GraphicsY = 105;
								}
								break;
							case 14:
								{
								//посох
								string[] data = GenerateWeaponName(8);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 30;
								GraphicsY = 0;
								}
								break;
							case 15:
								{
								//копьё
								string[] data = GenerateWeaponName(3);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 30;
								GraphicsY = 15;
								}
								break;
							case 16:
								{
								//топор
								string[] data = GenerateWeaponName(9);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 30;
								GraphicsY = 30;
								}
								break;
							case 17:
								{
								//посох
								string[] data = GenerateWeaponName(8);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 45;
								GraphicsY = 0;
								}
								break;
							case 18:
								{
								//меч
								string[] data = GenerateWeaponName(5);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 15;
								GraphicsY = 15;
								}
								break;
							case 19:
								//меч
								{
								string[] data = GenerateWeaponName(5);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 15;
								GraphicsY = 30;
								}
								break;
							case 20:
								{
								//меч								
								string[] data = GenerateWeaponName(5);
								selectedName = data[0];
								selectedDescr = data[1];
								
								GraphicsX = 15;
								GraphicsY = 45;
								}
								break;
						}
					}
					break;
				case 2:
					int rnd0 = srand.Next(0,8);
					
					if(rnd0 == 0)
					{
						selectedName = "Blood Shield";
		 				selectedDescr = "Enhanced Shield";
					}
					else if(rnd0 == 1)
					{
		 				selectedName = "Leather shield";
                 		selectedDescr = "Weak shield";
					}
					else if(rnd0 == 2)
					{
	       				selectedName = "Tomb shield";
                 		selectedDescr = "Weak shield";
					}
					else if(rnd0 == 3)
					{
                 		selectedName = "Knight Shield";
		 				selectedDescr = "Good Shield";
					}
					else if(rnd0 == 4)
					{
		 				selectedName = "Great Shield";
		 				selectedDescr = "Exellent Shield";
					}
					else if(rnd0 == 5)
					{
		 				selectedName = "Battle Shield";
		 				selectedDescr = "Better Shield";
					}
					else if(rnd0 == 6)
					{
	      		 		selectedName = "Dead Wall";
		 				selectedDescr = "Sacred Shield";
					}
					else if(rnd0 == 7)
					{
		 				selectedName = "Heavy Shield";
		 				selectedDescr = "Normal Shield";
					}
					else if(rnd0 == 8)
					{
		 				selectedName = "Power shield";
                 		selectedDescr = "Weak shield";
					}
						
					GraphicsID = 100007;
					int gen2 = rand.Next(1,3);
					switch(gen2)
					{
						case 1:
							GraphicsX = 15;
							GraphicsY = 105;
							break;
						case 2:
							GraphicsX = 30;
							GraphicsY = 0;
							break;
						case 3:
							GraphicsX = 30;
							GraphicsY = 15;
							break;
					}
					break;
				case 3:
					int rnd2 = srand.Next(0,12);
					if(rnd2 == 0)
					{
						selectedName = "Armor";
						selectedDescr = "CommonArmor";
					}
					else if(rnd2 == 1)
					{
						selectedName = "LightArmor";
						selectedDescr = "WeakArmor";
					}
					else if(rnd2 == 2)
					{
						selectedName = "WolfSkin";
						selectedDescr = "WeakArmor";
					}
					else if(rnd2 == 3)
					{
						selectedName = "LeatherArmor";
						selectedDescr = "WeakArmor";
					}
					else if(rnd2 == 4)
					{
						selectedName = "ChitinPlate";
						selectedDescr = "BodyArmor";
					}
					else if(rnd2 == 5)
					{
						selectedName = "AverageArmor";
						selectedDescr = "BodyArmor";
					}
					else if(rnd2 == 6)
					{
						selectedName = "BreastPlate";
						selectedDescr = "GoodArmor";
					}
					else if(rnd2 == 7)
					{
						selectedName = "KnightCloak";
						selectedDescr = "WeakArmor";
					}
					else if(rnd2 == 8)
					{
						selectedName = "GhostArmor";
						selectedDescr = "GodlyArmor";
					}
					else if(rnd2 == 9)
					{
						selectedName = "ChainMail";
						selectedDescr = "GoodArmor";
					}
					else if(rnd2 == 10)
					{
						selectedName = "HeavyArmor";
						selectedDescr = "SuperiorArmor";
					}
					else if(rnd2 == 11)
					{
						selectedName = "MageArmor";					
						selectedDescr = "MagicArmor";
					}
					else if(rnd2 == 12)
					{
						selectedName = "DwarfMail";
						selectedDescr = "QualityArmor";
					}
					
					
					GraphicsID = 100007;
					int gen3 = rand.Next(1,5);
					switch(gen3)
					{
						case 1:
							GraphicsX = 0;
							GraphicsY = 60;
							break;
						case 2:
							GraphicsX = 0;
							GraphicsY = 75;
							break;
						case 3:
							GraphicsX = 0;
							GraphicsY = 90;
							break;
						case 4:
							GraphicsX = 0;
							GraphicsY = 105;
							break;
						case 5:
							GraphicsX = 15;
							GraphicsY = 0;
							break;
					}
					break;
				case 4:
					int rnd1 = srand.Next(0,9);
					
					if(rnd1 == 0)
					{					
						selectedName = "Helm";
					    selectedDescr = "Common Helm";
					}
					else if(rnd1 == 1)
					{
		   				selectedName = "Light Helm";
		     			selectedDescr = "Weak Helm";
					}
					else if(rnd1 == 2)
					{
		   				selectedName = "Cold Face";
		     			selectedDescr = "Better Helm";
					}
					else if(rnd1 == 3)
					{
		   				selectedName = "Dead Crown";
		     			selectedDescr = "Sacred Helm";
					}
					else if(rnd1 == 4)
					{
		   				selectedName = "War Helm";
					    selectedDescr = "Exellent Helm";
					}
					else if(rnd1 == 5)
					{
		   				selectedName = "Magic Cap";
		    	 		selectedDescr = "Magic Helm";
					}
					else if(rnd1 == 6)
					{
		   				selectedName = "Full Helm";
		     			selectedDescr = "Valuable Helm";
					}
					else if(rnd1 == 7)
					{
		     			selectedName = "Protector";
		     			selectedDescr = "Regular Helm";
					}
					else if(rnd1 == 8)
					{
		     			selectedName = "Spiked Helm";
                    	selectedDescr = "Good Helm";
					}
					else if(rnd1 == 9)
					{
                    	selectedName = "Knight Helm";
		     			selectedDescr = "Quality Helm";
					}
					
					GraphicsID = 100007;
					int gen4 = rand.Next(1,4);
					switch(gen4)
					{
						case 1:
							GraphicsX = 0;
							GraphicsY = 0;
							break;
						case 2:
							GraphicsX = 0;
							GraphicsY = 15;
							break;
						case 3:
							GraphicsX = 0;
							GraphicsY = 30;
							break;
						case 4:
							GraphicsX = 0;
							GraphicsY = 45;
							break;
					}
					break;
				case 5:
					int rnd3 = srand.Next(0,7);
					
					if(rnd3 == 0)
					{
						selectedName = "Boots";
						selectedDescr = "NormalBoots";
					}
					else if(rnd3 == 1)
					{
						selectedName = "LightBoots";
						selectedDescr = "WeakBoots";
					}
					else if(rnd3 == 2)
					{
						selectedName = "LeatherBoots";
						selectedDescr = "PlainBoots";
					}
					else if(rnd3 == 3)
					{	
						selectedName = "Magicboots";
						selectedDescr = "Weakboots";
					}
					else if(rnd3 == 4)
					{
						selectedName = "ChainBoots";
						selectedDescr = "SuperiorBoots";
					}
					else if(rnd3 == 5)
					{
						selectedName = "MageBoots";
						selectedDescr = "MagicBoots";
					}
					else if(rnd3 == 6)
					{
						selectedName = "KnightBoots";
						selectedDescr = "QualityBoots";
					}
					else if(rnd3 == 7)
					{
						selectedName = "HeavyBoots";
						selectedDescr = "BetterBoots";
					}
					GraphicsID = 100007;
					int gen5 = rand.Next(1,3);
					switch(gen5)
					{
						case 1:
							GraphicsX = 15;
							GraphicsY = 15;
							break;
						case 2:
							GraphicsX = 15;
							GraphicsY = 30;
							break;
						case 3:
							GraphicsX = 15;
							GraphicsY = 45;
							break;
					}
					break;
				case 6:
					int rnd4 = srand.Next(0,5);
					if(rnd4 == 0)
					{
						selectedName = "Gloves";
						selectedDescr = "CommonGloves";
					}
					else if(rnd4 == 1)
					{
						selectedName = "LeatherGloves";
						selectedDescr = "SimpleGloves";
					}
					else if(rnd4 == 1)
					{
						selectedName = "MageGloves";
						selectedDescr = "MagicGloves";
					}
					else if(rnd4 == 1)
					{
						selectedName = "FireGloves";
						selectedDescr = "ValuableGloves";
					}
					else if(rnd4 == 1)
					{
						selectedName = "WiseHands";
						selectedDescr = "HolyGloves";
					}
					else if(rnd4 == 1)
					{
						selectedName = "LightGloves";
						selectedDescr = "PlainGloves";
					}
					GraphicsID = 100007;
					int gen6 = rand.Next(1,3);
					switch(gen6)
					{
						case 1:
							GraphicsX = 15;
							GraphicsY = 60;
							break;
						case 2:
							GraphicsX = 15;
							GraphicsY = 75;
							break;
						case 3:
							GraphicsX = 15;
							GraphicsY = 90;
							break;
					}
					break;
			}
			/* <end temp code>*/
			
			//pring dbg info
			/*Console.WriteLine("  ===Item DBG INFO");
			Console.WriteLine("     Req.Skill:"+reqSkill);
			Console.WriteLine("     Req.Magic:"+reqMagic);
			Console.WriteLine("     Health:"+lv1_stat_heal);
			Console.WriteLine("     Mana:"+lv1_stat_mana);
			Console.WriteLine("     Damage:"+lv1_stat_dmg);
			Console.WriteLine("     Attack:"+lv1_stat_atck);
			Console.WriteLine("     Defense:"+lv1_stat_def);
			Console.WriteLine("     Skill:"+lv1_stat_skill);
			Console.WriteLine("     Magic:"+lv1_stat_magic);
			Console.WriteLine("     Health Regen:"+hregen);
			Console.WriteLine("     Mana Regen:"+mregen);*/
			
			gold = level * rand.Next(1,100);			
			string release = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Item>
	<type>"+type+@"</type>
	<set_id>0</set_id>
	<graphics_id>"+GraphicsID+@"</graphics_id>
	<graphics_x>"+GraphicsX+@"</graphics_x>
	<graphics_y>"+GraphicsY+@"</graphics_y>
	<name>"+selectedName+@"</name>
	<description>"+selectedDescr+@"</description>
	<available_status>all</available_status>
	<can_sell>1</can_sell>
	<can_drop>1</can_drop>
	<max_units>1</max_units>
	<price>"+gold+@"</price>
	<health_effect>"+lv1_stat_heal+@"</health_effect>
	<mana_effect>"+lv1_stat_mana+@"</mana_effect>
	<attack_effect>"+lv1_stat_atck+@"</attack_effect>
	<defense_effect>"+lv1_stat_def+@"</defense_effect>
	<damage_effect>"+lv1_stat_dmg+@"</damage_effect>
	<skill_effect>"+lv1_stat_skill+@"</skill_effect>
	<magic_effect>"+lv1_stat_magic+@"</magic_effect>
	<healthregenerate_effect>"+hregen+@"</healthregenerate_effect>
	<manaregenerate_effect>"+mregen+@"</manaregenerate_effect>
	<action_effect_1>0</action_effect_1>
	<action_effect_1_data></action_effect_1_data>
	<action_effect_2>0</action_effect_2>
	<action_effect_2_data></action_effect_2_data>
	<effect_duration>0</effect_duration>
	<required_skill>"+reqSkill+@"</required_skill>
	<required_magic>"+reqMagic+@"</required_magic>
	<frequency>"+frequency+@"</frequency>
	<range>"+range+@"</range>
	<premium>0</premium>
	<usage_type>1</usage_type>
	<static>false</static>
</Item>
";
			StreamWriter sw=new StreamWriter("Data/Items/Dyn/Item_"+generatedId+".xml");
			sw.Write(release);
			sw.Flush();
			sw.Close();
			
			
			
			int objectID=rand.Next(100000,999999);
			Items.Item it=new RhynnServer.Code.Items.Item();
            it.id=generatedId;
            it.ObjectID=objectID;
            it.units=1;
			it.GraphicsID=GraphicsID;
			it.GraphicsX=GraphicsX;
			it.GraphicsY=GraphicsY;
			it.usageType=1;			
			int xp = srand.Next(0,10);
			int yp = srand.Next(0,10);			
			it.x=who.mobj.X-xp;
			it.y=who.mobj.Y-yp;						
			Items.ItemCL.AddItemToAll(who.MapID,it);
		}
		
		public static void DropScroll(Mob who)
		{
			int itemID = 0;
			
			Random rand=new Random();
			itemID = rand.Next(0,36);

			int objectID=rand.Next(100000,999999);
			Items.Item it=new RhynnServer.Code.Items.Item();
            it.id=itemID;
            it.ObjectID=objectID;
            it.units=3;
            		
            int GraphicsID=int.Parse(XmlReader.GetValueFromXml(Items.IPathHelper.GetOptPath(itemID),"/Item/graphics_id"));
			int GraphicsX=int.Parse(XmlReader.GetValueFromXml(Items.IPathHelper.GetOptPath(itemID),"/Item/graphics_x"));
			int GraphicsY=int.Parse(XmlReader.GetValueFromXml(Items.IPathHelper.GetOptPath(itemID),"/Item/graphics_y"));
			int usageType=int.Parse(XmlReader.GetValueFromXml(Items.IPathHelper.GetOptPath(itemID),"/Item/usage_type"));
            		
			it.GraphicsID=GraphicsID;
			it.GraphicsX=GraphicsX;
			it.GraphicsY=GraphicsY;
			it.usageType=usageType;
			int xp = srand.Next(1,5);//original 0,10
			int yp = srand.Next(1,5);//0,10
			
			it.x=who.mobj.X-xp;
			it.y=who.mobj.Y-yp;
						
			Items.ItemCL.AddItemToAll(who.MapID,it);
		}
		
		public static void DropPotion(Mob who)
		{
			int itemID = 37;
			
			Random rand=new Random();
			int what = rand.Next(0,1);
			if(what==0)
			{
				itemID = 37;
			}
			else
			{
				itemID = 39;
			}
			
			int objectID=rand.Next(100000,999999);
			Items.Item it=new RhynnServer.Code.Items.Item();
            it.id=itemID;
            it.ObjectID=objectID;
            it.units=3;
            		
            int GraphicsID=int.Parse(XmlReader.GetValueFromXml(Items.IPathHelper.GetOptPath(itemID),"/Item/graphics_id"));
			int GraphicsX=int.Parse(XmlReader.GetValueFromXml(Items.IPathHelper.GetOptPath(itemID),"/Item/graphics_x"));
			int GraphicsY=int.Parse(XmlReader.GetValueFromXml(Items.IPathHelper.GetOptPath(itemID),"/Item/graphics_y"));
			int usageType=int.Parse(XmlReader.GetValueFromXml(Items.IPathHelper.GetOptPath(itemID),"/Item/usage_type"));
            		
			it.GraphicsID=GraphicsID;
			it.GraphicsX=GraphicsX;
			it.GraphicsY=GraphicsY;
			it.usageType=usageType;
			int xp = srand.Next(1,5);//original 0,10
			int yp = srand.Next(1,5);//0,10
			
			it.x=who.mobj.X-xp;
			it.y=who.mobj.Y-yp;
						
			Items.ItemCL.AddItemToAll(who.MapID,it);
		}
		
		public static void DropGold(Mob who,int amount)
		{
			Random rnd=new Random();
			int objectID=rnd.Next(100000,999999);
			Items.Item it=new RhynnServer.Code.Items.Item();
            it.id=-1;
            it.ObjectID=objectID;
            it.units=amount;
            		
            int GraphicsID=100006;
			int GraphicsX=45;
			int GraphicsY=90;
			int usageType=3;
            		
			it.GraphicsID=GraphicsID;
			it.GraphicsX=GraphicsX;
			it.GraphicsY=GraphicsY;
			it.usageType=usageType;
			int xp = srand.Next(1,5);//original 0,10
			int yp = srand.Next(1,5);//0,10
			
			it.x=who.mobj.X-xp;
			it.y=who.mobj.Y-yp;
						
			Items.ItemCL.AddItemToAll(who.MapID,it);
		}
		
		//main DropFunction
		private static Hashtable item_drop(int type, int level, int rarity)
		{
			//define vars with types(yep,it's not php...)
			float rsk = 0;
			float rmg = 0;
			float hp = 0;
			float mana = 0;
			float dmg = 0;
			float atk = 0;
			float def = 0;
			float sk = 0;
			float mg = 0;
			float hf = 0;
			float mf = 0;
			
			level = level+1;
			//now need to us make different items stats (not only every time atk dmg etc.)
			// you can up or decrease hp mana dmg atk def sk mg hf and mf value, but warning, do not do it more. (count 
			//if you want to make own best value moblevel*1.2 if you want to set maximum dmg for weapons. i think you understand.)
			
			float hpz = 40;
			float manaz = 20;
			float dmgz = 40;
			float atkz = 40;
			float defz = 40;
			float skz = 40;
			float mgz = 40;
			
			int random_nr = ChanceCalculator.rnd.Next(0,rarity); //make random value
			if(type == 1)//making stats for helm
			{
				//maximum value here can be
				hpz = 50;
				manaz = 20;
				dmgz = 40;
				atkz = 40;
				defz = 60;
				skz = 40;
				mgz = 40;
				int rand_hp = ChanceCalculator.rnd.Next(0,rarity); //make random value only for hp
				if(random_nr == rarity && rand_hp == rarity) //checking health can be added to stats or no
				{ 
					hp = 0.4f; //then can be added maximum health value for 0 lvl 
				}
				else
				{
					hp = 0; // else will be not added this stat
				}
				int rand_mana = ChanceCalculator.rnd.Next(0,rarity); //make random value only for mana
				if(random_nr == rarity && rand_mana == rarity) //checking mana can be added to stats or no
				{ 
					mana = 0.1f; //then can be added maximum mana value for 0 lvl 
				}
				else
				{
					mana = 0; // else will be not added this stat
				}
				int rand_dmg = ChanceCalculator.rnd.Next(0,rarity); //make random value only for dmg
				if(random_nr == rarity && rand_dmg == rarity) //checking damage can be added to stats or no
				{
					dmg = 0.3f; //then can be added maximum damage value for 0 lvl 
				}
				else
				{
					dmg = 0; // else will be not added this stat
				}
				int rand_atk = ChanceCalculator.rnd.Next(0,rarity); //make random value only for atk
				if(random_nr == rarity && rand_atk == rarity) //checking attack can be added to stats or no
				{ 
					atk = 0.3f; //then can be added maximum attack value for 0 lvl 
				}
				else
				{
					atk = 0; // else will be not added this stat
				}
				def = 0.5f; //default value, this stat will be every time
				int rand_sk = ChanceCalculator.rnd.Next(0,rarity); //make random value only for skill
				if(random_nr == rarity && rand_sk == rarity) //checking skill can be added to stats or no
				{
					sk = 0.3f; //then can be added maximum skill value for 0 lvl 
				}
				else
				{
					sk = 0; // else will be not added this stat
				}
				int rand_mg = ChanceCalculator.rnd.Next(0,rarity); //make random value only for magic (stat)
				if(random_nr == rarity && rand_hp == rarity) //checking magic can be added to stats or no
				{
					mg = 0.3f; //then can be added maximum magic value for 0 lvl 
				}
				else
				{
					mg = 0; // else will be not added this stat
				}
			}
			else if(type == 2)//making stats for armor
			{
				//maximum values here can be
				hpz = 60;
				manaz = 20;
				dmgz = 70;
				atkz = 50;
				defz = 80;
				skz = 50;
				mgz = 50;
				int rand_hp = ChanceCalculator.rnd.Next(0,rarity); //make random value only for hp
				if(random_nr == rarity && rand_hp == rarity) //checking health can be added to stats or no
				{
					hp = 0.5f; //then can be added maximum health value for 0 lvl 
				}
				else
				{
					hp = 0; // else will be not added this stat
				}
				int rand_mana = ChanceCalculator.rnd.Next(0,rarity); //make random value only for mana
				if(random_nr == rarity && rand_mana == rarity) //checking mana can be added to stats or no
				{ 
					mana = 0.1f; //then can be added maximum mana value for 0 lvl 
				}
				else
				{
					mana = 0; // else will be not added this stat
				}
				int rand_dmg = ChanceCalculator.rnd.Next(0,rarity); //make random value only for dmg
				if(random_nr == rarity && rand_dmg == rarity) //checking damage can be added to stats or no
				{ 
					dmg = 0.6f; //then can be added maximum damage value for 0 lvl 
				}
				else
				{
					dmg = 0; // else will be not added this stat
				}
				int rand_atk = ChanceCalculator.rnd.Next(0,rarity); //make random value only for atk
				if(random_nr == rarity && rand_atk == rarity) //checking attack can be added to stats or no
				{ 
					atk = 0.4f; //then can be added maximum attack value for 0 lvl 
				}
				else
				{
					atk = 0; // else will be not added this stat
				}
				def = 0.7f; //default value, this stat will be every time
				int rand_sk = ChanceCalculator.rnd.Next(0,rarity); //make random value only for skill
				if(random_nr == rarity && rand_sk == rarity) //checking skill can be added to stats or no
				{ 
					sk = 0.4f; //then can be added maximum skill value for 0 lvl 
				}
				else
				{
					sk = 0; // else will be not added this stat
				}
				int rand_mg = ChanceCalculator.rnd.Next(0,rarity); //make random value only for magic (stat)
				if(random_nr == rarity && rand_hp == rarity) //checking magic can be added to stats or no
				{ 
					mg = 0.4f; //then can be added maximum magic value for 0 lvl 
				}
				else
				{
					mg = 0; // else will be not added this stat
				}
			}
			else if(type == 3)//making stats for weapon
			{
				//maximum values here can be
				hpz = 50;
				manaz = 40;
				dmgz = 130;
				atkz = 130;
				defz = 40;
				skz = 50;
				mgz = 50;
				int rand_hp = ChanceCalculator.rnd.Next(0,rarity); //make random value only for hp
				if(random_nr == rarity && rand_hp == rarity) //checking health can be added to stats or no
				{ 
					hp = 0.4f; //then can be added maximum health value for 0 lvl 
				}
				else
				{
					hp = 0; // else will be not added this stat
				}
				int rand_mana = ChanceCalculator.rnd.Next(0,rarity); //make random value only for mana
				if(random_nr == rarity && rand_mana == rarity) //checking mana can be added to stats or no
				{ 
					mana = 0.3f; //then can be added maximum mana value for 0 lvl 
				}
				else
				{
					mana = 0; // else will be not added this stat
				}
				dmg = 1.5f; //default value, this stat will be every time
				atk = 1.5f; //default value, this stat will be every time
				int rand_def = ChanceCalculator.rnd.Next(0,rarity); //make random value only for def
				if(random_nr == rarity && rand_def == rarity) //checking defence can be added to stats or no
				{ 
					def = 0.3f; //then can be added maximum defence value for 0 lvl 
				}
				else
				{
					def = 0; // else will be not added this stat
				}
				int rand_sk = ChanceCalculator.rnd.Next(0,rarity); //make random value only for skill
				if(random_nr == rarity && rand_sk == rarity) //checking skill can be added to stats or no
				{ 
					sk = 0.4f; //then can be added maximum skill value for 0 lvl 
				}
				else
				{
					sk = 0; // else will be not added this stat
				}
				int rand_mg = ChanceCalculator.rnd.Next(0,rarity); //make random value only for magic (stat)
				if(random_nr == rarity && rand_hp == rarity) //checking magic can be added to stats or no
				{ 
					mg = 0.4f; //then can be added maximum magic value for 0 lvl 
				}
				else
				{
					mg = 0; // else will be not added this stat
				}
			}
			else if(type == 4)//making stats for shield
			{
				//maximum values can be
			    hpz = 30;
				manaz = 20;
				dmgz = 40;
				atkz = 50;
				defz = 120;
				skz = 40;
				mgz = 70;
				int rand_hp = ChanceCalculator.rnd.Next(0,rarity); //make random value only for hp
				if(random_nr == rarity && rand_hp == rarity) //checking health can be added to stats or no
				{ 
					hp = 0.2f; //then can be added maximum health value for 0 lvl 
				}
				else
				{
					hp = 0; // else will be not added this stat
				}
				int rand_mana = ChanceCalculator.rnd.Next(0,rarity); //make random value only for mana
				if(random_nr == rarity && rand_mana == rarity) //checking mana can be added to stats or no
				{ 
					mana = 0.1f; //then can be added maximum mana value for 0 lvl 
				}
				else
				{
					mana = 0; // else will be not added this stat
				}
				int rand_dmg = ChanceCalculator.rnd.Next(0,rarity); //make random value only for dmg
				if(random_nr == rarity && rand_dmg == rarity) //checking damage can be added to stats or no
				{ 
					dmg = 0.3f; //then can be added maximum damage value for 0 lvl 
				}
				else
				{
					dmg = 0; // else will be not added this stat
				}
				int rand_atk = ChanceCalculator.rnd.Next(0,rarity); //make random value only for atk
				if(random_nr == rarity && rand_atk == rarity) //checking attack can be added to stats or no
				{ 
					atk = 0.4f; //then can be added maximum attack value for 0 lvl 
				}
				else
				{
					atk = 0; // else will be not added this stat
				}
				def = 1.1f; //default value, this stat will be every time
				int rand_sk = ChanceCalculator.rnd.Next(0,rarity); //make random value only for skill
				if(random_nr == rarity && rand_sk == rarity) //checking skill can be added to stats or no
				{
					sk = 0.3f; //then can be added maximum skill value for 0 lvl 
				}
				else
				{
					sk = 0; // else will be not added this stat
				}
				int rand_mg = ChanceCalculator.rnd.Next(0,rarity); //make random value only for magic (stat)
				if(random_nr == rarity && rand_hp == rarity) //checking magic can be added to stats or no
				{
					mg = 0.6f; //then can be added maximum magic value for 0 lvl 
				}
				else
				{
					mg = 0; // else will be not added this stat
				}
			}
			else if(type == 5)//making stats for glove
			{
				//maximum values here can be
				hpz = 40;
				manaz = 20;
				dmgz = 40;
				atkz = 40;
				defz = 40;
				skz = 40;
				mgz = 40;
				int rand_hp = ChanceCalculator.rnd.Next(0,rarity); //make random value only for hp
				if(random_nr == rarity && rand_hp == rarity) //checking health can be added to stats or no
				{ 
					hp = 0.3f; //then can be added maximum health value for 0 lvl 
				}
				else
				{
					hp = 0; // else will be not added this stat
				}
				int rand_mana = ChanceCalculator.rnd.Next(0,rarity); //make random value only for mana
				if(random_nr == rarity && rand_mana == rarity) //checking mana can be added to stats or no
				{ 
					mana = 0.1f; //then can be added maximum mana value for 0 lvl 
				}
				else
				{
					mana = 0; // else will be not added this stat
				}
				int rand_dmg = ChanceCalculator.rnd.Next(0,rarity); //make random value only for dmg
				if(random_nr == rarity && rand_dmg == rarity) //checking damage can be added to stats or no
				{ 
					dmg = 0.3f; //then can be added maximum damage value for 0 lvl 
				}
				else
				{
					dmg = 0; // else will be not added this stat
				}
				int rand_atk = ChanceCalculator.rnd.Next(0,rarity); //make random value only for atk
				if(random_nr == rarity && rand_atk == rarity) //checking attack can be added to stats or no
				{ 
					atk = 0.3f; //then can be added maximum attack value for 0 lvl 
				}
				else
				{
					atk = 0; // else will be not added this stat
				}
				def = 0.3f; //default value, this stat will be every time
				int rand_sk = ChanceCalculator.rnd.Next(0,rarity); //make random value only for skill
				if(random_nr == rarity && rand_sk == rarity) //checking skill can be added to stats or no
				{ 
					sk = 0.3f; //then can be added maximum skill value for 0 lvl 
				}
				else
				{
					sk = 0; // else will be not added this stat
				}
				int rand_mg = ChanceCalculator.rnd.Next(0,rarity); //make random value only for magic (stat)
				if(random_nr == rarity && rand_hp == rarity) //checking magic can be added to stats or no
				{ 
					mg = 0.3f; //then can be added maximum magic value for 0 lvl 
				}
				else
				{
					mg = 0; // else will be not added this stat
				}
			}
			else if(type == 6)//making stats for boots
			{
				//maximum values here
				hpz = 40;
				manaz = 20;
				dmgz = 50;
				atkz = 50;
				defz = 40;
				skz = 30;
				mgz = 30;
				int rand_hp = ChanceCalculator.rnd.Next(0,rarity); //make random value only for hp
				if(random_nr == rarity && rand_hp == rarity) //checking health can be added to stats or no
				{ 
					hp = 0.5f; //then can be added maximum health value for 0 lvl 
				}
				else
				{
					hp = 0; // else will be not added this stat
				}
				int rand_mana = ChanceCalculator.rnd.Next(0,rarity); //make random value only for mana
				if(random_nr == rarity && rand_mana == rarity) //checking mana can be added to stats or no
				{ 
					mana = 0.1f; //then can be added maximum mana value for 0 lvl 
				}
				else
				{
					mana = 0; // else will be not added this stat
				}
				int rand_dmg = ChanceCalculator.rnd.Next(0,rarity); //make random value only for dmg
				if(random_nr == rarity && rand_dmg == rarity) //checking damage can be added to stats or no
				{ 
					dmg = 0.4f; //then can be added maximum damage value for 0 lvl 
				}
				else
				{
					dmg = 0; // else will be not added this stat
				}
				int rand_atk = ChanceCalculator.rnd.Next(0,rarity); //make random value only for atk
				if(random_nr == rarity && rand_atk == rarity) //checking attack can be added to stats or no
				{ 
					atk = 0.4f; //then can be added maximum attack value for 0 lvl 
				}
				else
				{
					atk = 0; // else will be not added this stat
				}
				def = 0.3f; //default value, this stat will be every time
				int rand_sk = ChanceCalculator.rnd.Next(0,rarity); //make random value only for skill
				if(random_nr == rarity && rand_sk == rarity) //checking skill can be added to stats or no
				{ 
					sk = 0.2f; //then can be added maximum skill value for 0 lvl 
				}
				else
				{
					sk = 0; // else will be not added this stat
				}
				int rand_mg = ChanceCalculator.rnd.Next(0,rarity); //make random value only for magic (stat)
				if(random_nr == rarity && rand_mg == rarity) //checking magic can be added to stats or no
				{ 
					mg = 0.2f; //then can be added maximum magic value for 0 lvl 
				}
				else
				{
					mg = 0; // else will be not added this stat
				}
			}
			else //item not exists
			{
				hp = 0;
				mana = 0;
				dmg = 0;
				atk = 0;
				def = 0;
				sk = 0;
				mg = 0;
			}
			float rand_mf = ChanceCalculator.rnd.Next(0,rarity); //make random value only for magic fill
			if(random_nr == rarity && rand_mf == rarity) //checking magic fill can be added to stats or no
			{ 
				mf = 0.2f; //then can be added maximum magic fill value for 0 lvl 
			}
			else
			{
				mf = 0; // else will be not added this stat
			}
			float rand_hf = ChanceCalculator.rnd.Next(0,rarity); //make random value only for health fill
			if(random_nr == rarity && rand_hf == rarity) //checking health fill can be added to stats or no
			{ 
				hf = 0.2f; //then can be added maximum magic fill value for 0 lvl 
			}
			else
			{
				hf = 0; // else will be not added this stat
			}
			// now need to make real stats
			if(hp > 0)//checking that stats was added or not
			{
				//then make random health number
				float hp_max = hp*level; //set maximum value of health
				float hp_min = 0.1f; //set minimum value of health
				hp_max = hp_max*10; //making value for set max number perfectly
				hp_min = hp_min*10; //making value for set min number perfectly
				float rand_hp = ChanceCalculator.Next(hp_min*10,hp_max*10)/10; //making random value of health
				hp = rand_hp/10; // true value;
			}
			else
			{
				hp = 0;
			}
			if(mana > 0)//checking that stats was added or not
			{
				//then make random mana number
				float mana_max = mana*level; //set maximum value of mana
				float mana_min = 0.1f; //set minimum value of mana
				mana_max = mana_max*10; //making value for set max number perfectly
				mana_min = mana_min*10; //making value for set min number perfectly
				float rand_mana = ChanceCalculator.Next(mana_min*10,mana_max*10)/10; //making random value of mana
				mana = rand_mana/10; // true value;
			}
			else
			{
				mana = 0;
			}
			if(dmg > 0)//checking that stats was added or not
			{
				//then make random damage number
				float dmg_max = dmg*level; //set maximum value of damage
				float dmg_min = 0.1f; //set minimum value of dmg
				dmg_max = dmg_max*10; //making value for set max number perfectly
				dmg_min = dmg_min*10; //making value for set min number perfectly
				float rand_dmg = ChanceCalculator.Next(dmg_min*10,dmg_max*10)/10; //making random value of damage
				dmg = rand_dmg/10; // true value;
			}
			else
			{
				dmg = 0;
			}
			if(atk > 0)//checking that stats was added or not
			{
				//then make random attack number
				float atk_max = atk*level; //set maximum value of attack
				float atk_min = 0.1f; //set minimum value of atk
				atk_max = atk_max*10; //making value for set max number perfectly
				atk_min = atk_min*10; //making value for set min number perfectly
				float rand_atk = ChanceCalculator.Next(atk_min*10,atk_max*10)/10; //making random value of attack
				atk = rand_atk/10; // true value;
			}
			else
			{
				atk = 0;
			}
			if(def > 0)//checking that stats was added or not
			{
				//then make random defence number
				float def_max = def*level; //set maximum value of defence
				float def_min = 0.1f; //set minimum value of def
				def_max = def_max*10; //making value for set max number perfectly
				def_min = def_min*10; //making value for set min number perfectly
				float rand_def = ChanceCalculator.Next(def_min*10,def_max*10)/10; //making random value of defence
				def = rand_def/10; // true value;
			}
			else
			{
				def = 0;
			}
			if(sk > 0)//checking that stats was added or not
			{
				//then make random skill number
				float sk_max = sk*level; //set maximum value of skill
				float sk_min = 0.1f; //set minimum value of sk
				sk_max = sk_max*10; //making value for set max number perfectly
				sk_min = sk_min*10; //making value for set min number perfectly
				float rand_sk = ChanceCalculator.Next(sk_min*10,sk_max*10)/10; //making random value of skill
				sk = rand_sk/10; // true value;
			}
			else
			{
				sk = 0;
			}
			if(mg > 0)//checking that stats was added or not
			{
				//then make random magic number
				float mg_max = mg*level; //set maximum value of magic
				float mg_min = 0.1f; //set minimum value of mg
				mg_max = mg_max*10; //making value for set max number perfectly
				mg_min = mg_min*10; //making value for set min number perfectly
				float rand_mg = ChanceCalculator.Next(mg_min*10,mg_max*10)/10; //making random value of magic
				mg = rand_mg/10; // true value;
			}
			else
			{
				mg = 0;
			}
			if(hf > 0)//checking that stats was added or not
			{
				//then make random health fill number
				float hf_max = hf*level; //set maximum value of health fill
				float hf_min = 0.1f; //set minimum value of hf
				hf_max = hf_max*10; //making value for set max number perfectly
				hf_min = hf_min*10; //making value for set min number perfectly
				rand_hf = ChanceCalculator.Next(hf_min*10,hf_max*10)/10; //making random value of health fill
				hf = rand_hf/10; // true value;
			}
			else
			{
				hf = 0;
			}
			if(mf > 0)//checking that stats was added or not
			{
				//then make random mana fill number
				float mf_max = mf*level; //set maximum value of mana fill
				float mf_min = 0.1f; //set minimum value of mf
				mf_max = mf_max*10; //making value for set max number perfectly
				mf_min = mf_min*10; //making value for set min number perfectly
				rand_mf = ChanceCalculator.Next(mf_min*10,mf_max*10)/10; //making random value of mana fill
				mf = rand_mf/10; // true value;
			}
			else
			{
				mf = 0;
			}
				// now need to set skill and magic requements for items:
				
				//now we set procents how much from full value of stats is setted
			float hfz = 30; //max value in healthfill
			float mfz = 30; // max value in mana fill
			
			float hpps = 0;
			int hpps2 = 0;
			if(hp > 0)
			{
				hpps = 100/hpz;
				hpps2 = (int)(hp*hpps);
			}
			else
			{
				hpps2 = 0;
			}
			float manaps = 0;
			int manaps2 = 0;
			if(mana > 0)
			{
				manaps = 100/manaz;
				manaps2 = (int)(mana*manaps);
			}
			else
			{
				manaps2 = 0;
			}
			float dmgps = 0;
			int dmgps2 = 0;
			if(dmg > 0)
			{
				dmgps = 100/dmgz;
				dmgps2 = (int)(dmg*dmgps);
			}
			else
			{
				dmgps2 = 0;
			}
			float atkps = 0;
			int atkps2 = 0;
			if(atk > 0)
			{
				atkps = 100/atkz;
				atkps2 = (int)(atk*atkps);
			}
			else
			{
				atkps2 = 0;
			}
			float defps = 0;
			int defps2 = 0;
			if(def > 0)
			{
				defps = 100/defz;
				defps2 = (int)(def*defps);
			}
			else
			{
				defps2 = 0;
			}
			float skps = 0;
			int skps2 = 0;
			if(sk > 0)
			{
				skps = 100/skz;
				skps2 = (int)(sk*skps);
			}
			else
			{
				skps2 = 0;
			}
			float mgps = 0;
			int mgps2 = 0;
			if(mg > 0)
			{
				mgps = 100/mgz;
				mgps2 = (int)(mg*mgps);
			}
			else
			{
				mgps2 = 0;
			}
			float hfps = 0;
			int hfps2 = 0 ;
			if(hf > 0)
			{
				hfps = 100/hfz;
				hfps2 = (int)(hf*hfps);
			}
			else
			{
				hfps2 = 0;
			}
			float mfps = 0;
			int mfps2 = 0;
			if(mf > 0)
			{
				mfps = 100/mfz;
				mfps2 = (int)(mf*mfps);
			}
			else
			{
				mfps2 = 0;
			}
			//now need to set skill and magic requements
			if(level < 5)
			{
				// if mob level is 0-4 then will be no requements
				rsk = 0;
				rmg = 0;
			}
			else
			{
				int min = 0;
				int vimd = 0;
				int vimd2 = 0;
				int vimd3 = 0;
				int vimd2s = 0;
				int vimd3s = 0;
				int rsks = 0;
				int rmgs = 0;
				min = new int[]{hpps2,manaps2,dmgps2,atkps2,defps2,skps2,mgps2,hfps2,mfps2}.Max(); //minimum value for requements
				if(min > 40)
				{
					vimd = min-40;
				}
				else
				{
					vimd = (int)(min/2);
				}
				vimd2 = 160 * vimd;
				vimd3 = vimd2 / 100;
				vimd3 = (int)(vimd3); //set procents how much minimum requements can be added
				vimd2s = 160 * min;
				vimd3s = vimd2s / 100;
				vimd3s = (int)(vimd3s);// set procents how much maximum requements can be added
				rsks = vimd3s;
				rmgs = vimd3s;
				//echo vimd3.'/'.rmgs.'<br>';
				if(ChanceCalculator.rnd.Next(0,1) == 1)
				{
					rmg = ChanceCalculator.Next((float)vimd3,(float)rmgs);
					rsk = ChanceCalculator.Next((float)vimd3,(float)rsks);
					//echo 'a/';
				}
				else
				{
					if(ChanceCalculator.rnd.Next(0,3) == 3)
					{
						rsk = 0;
					}
					else
					{
						rsk = ChanceCalculator.Next((float)vimd3,(float)rsks);
					}
					if(ChanceCalculator.rnd.Next(0,3) == 3)
					{
						rmg = 0;
					}
					else
					{
						rmg = ChanceCalculator.Next((float)vimd3,(float)rmgs);
					}
					//echo 'b/';
					if(rsk == 0 && rmg == 0)
					{
						int rq = ChanceCalculator.rnd.Next(0,1);
						if(rq == 1)
						{
							rsk = ChanceCalculator.Next((float)vimd3,(float)rsks);
						}
						else
						{
							rmg = ChanceCalculator.Next((float)vimd3,(float)rmgs);
						}
					//echo 'c/';
					}
				}
				if(rsk > 160)
				{
					rsk = 160;
				}
				if(rmg > 160)
				{
					rmg = 160;
				}
			}
			
			int _rsk = (int)(rsk*10);
			int _rmg = (int)(rmg*10);
			int _hp = (int)(hp*10);
			int _mana = (int)(mana*10);
			int _dmg = (int)(dmg*10);
			int _atk = (int)(atk*10);
			int _def = (int)(def*10);
			int _sk = (int)(sk*10);
			int _mg = (int)(mg*10);
			int _hf = (int)(hf*10);
			int _mf = (int)(mf*10);
			
			Hashtable result = new Hashtable();
			result.Add("rskill",_rsk);
			result.Add("rmagic",_rmg);
			result.Add("hp",_hp);			
			result.Add("mana",_mana);
			result.Add("dmg",_dmg);
			result.Add("atk",_atk);
			result.Add("def",_def);
			result.Add("skill",_sk);
			result.Add("magic",_mg);
			result.Add("hregen",_hf);
			result.Add("mregen",_mf);
			return result;
		}
	}
}