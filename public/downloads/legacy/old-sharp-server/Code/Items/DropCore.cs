/*
 * Created by STeeL
 * 2013
 * Please read file Readme.txt before use this source file
 * 
 * (C) STeeL-Team 2013
 */
using System;
using System.IO;
using RhynnServer.Code.Core;
using RhynnServer.Code.AI;
using RhynnServer.Code.Network;

namespace RhynnServer.Code.Items
{
	/// <summary>
	/// Description of DropCore.
	/// </summary>
	public class DropCore
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
				int mediumChance = 0;
				if(Legendary)
				{
					LegendaryChance+=20;
				}
				if(Plevel<=40)
				{
					mediumChance = Plevel*2;
				}
				else
				{
					mediumChance = 30;//55%
				}
				int lowerChance = 40;//lower item chance 70%
				int goldChance = 50;//80%
				int potionChance = 45;//80%
				
				bool IsLegendary = ChanceCalculator.Calculate(LegendaryChance);
				bool IsMedium = ChanceCalculator.Calculate(mediumChance);
				bool IsLower = ChanceCalculator.Calculate(lowerChance);
				bool IsGold = ChanceCalculator.Calculate(goldChance);
				bool IsPotion = ChanceCalculator.Calculate(potionChance);
				
				if(IsLegendary)
				{
					try
					{
						if(IsGold)
						{
							DropGold(who,int.Parse(XmlReader.GetValueFromXml("Data/Mobs/"+who.MobID.ToString()+".xml","/Mob/DropGOLD")));
						}
						DropItemLegendary(who,level);
					}
					catch
					{
						
					}
					return;
				}
				if(IsMedium)
				{
					try
					{
						if(IsGold)
						{
							DropGold(who,int.Parse(XmlReader.GetValueFromXml("Data/Mobs/"+who.MobID.ToString()+".xml","/Mob/DropGOLD")));
						}
					}
					catch
					{
						
					}
					
					DropItemMedium(who,level);
					
					return;
				}
				if(IsLower)
				{
					try
					{
						if(IsGold)
						{
							DropGold(who,int.Parse(XmlReader.GetValueFromXml("Data/Mobs/"+who.MobID.ToString()+".xml","/Mob/DropGOLD")));
						}
					}
					catch
					{
						
					}
					
					DropItemLower(who,level);
					
					return;
				}				
				if(IsPotion)
				{
					DropPotion(who);
					return;
				}
			}
			catch(Exception ex)
			{
				ServConsole.Debug(ex.ToString());
			}
		}
		
		public static void DropItemLower(Mob who,int level)
		{
			Random rand=new Random();
			int rndCof = rand.Next(1,9);
			int gold = 0;
			int lv1_stat_atck = 2;
			int lv1_stat_dmg = 1;
			int lv1_stat_def = 2;
			int lv1_stat_heal = 3;
			int lv1_stat_mana = 2;
			int frequency = 120;
			int range = 10;//ближнее оружие
			
			int GraphicsID = 100006;
			
			string[] itemNames = 
			{
				"Light weapon",
				"Light weapon",
				"Light armor",
				"Light boots",
				"Light gloves",
				"Light helmet",
				"Light shield"
			};
			
			string[] itemDescr = 
			{
				"near fighting weapon",
				"far fighting weapon",
				"not strong armor",
				"not strong boots",
				"not strong gloves",
				"not strong helmet",
				"not strong shield"
			};
			
			string selectedName = "";
			string selectedDescr = "";
			int generatedId = rand.Next(100000,999999);
			
			int clType = 0;
			
			//generate item
			//0 - ближние оружие,1 - дальнее оружие,2 - нагрудник,3 - боты,4 - перчи,5 - шлем,6 - щит
			int type = rand.Next(0,6);
			gold = level * rand.Next(1,100);
			switch(type)
			{
				case 0:
					clType = 1;
					rndCof = rand.Next(1,9);
					lv1_stat_atck *= rand.Next(1,level)+rndCof;
					rndCof = rand.Next(1,9);
					lv1_stat_dmg *= rand.Next(1,level)+rndCof;
					rndCof = rand.Next(1,level/2);
					frequency +=rndCof;
					
					//null another
					lv1_stat_mana = 0;
					lv1_stat_heal = 0;
					lv1_stat_def = 0;
					
					
					selectedName = itemNames[0];
					selectedDescr = itemDescr[0];
					break;
				case 1:
					clType = 1;
					rndCof = rand.Next(1,9);
					lv1_stat_atck *= rand.Next(1,level)+rndCof;
					rndCof = rand.Next(1,9);
					lv1_stat_dmg *= rand.Next(1,level)+rndCof;
					rndCof = rand.Next(1,9);
					range = 20 + rndCof;
					
					//null another
					lv1_stat_mana = 0;
					lv1_stat_heal = 0;
					lv1_stat_def = 0;					
					
					
					
					
					selectedName = itemNames[1];
					selectedDescr = itemDescr[1];
					break;
				case 2:
					clType = 3;
					GraphicsID = 100007;
					rndCof = rand.Next(0,9);
					lv1_stat_def *= rand.Next(1,level)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_heal *= rand.Next(0,level)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_mana *= rand.Next(0,level)+rndCof;
					
					//null another
					lv1_stat_atck = 0;
					lv1_stat_dmg = 0;
					
					
					
					
					selectedName = itemNames[2];
					selectedDescr = itemDescr[2];
					break;
				case 3:
					GraphicsID = 100007;
					rndCof = rand.Next(0,9);
					lv1_stat_def *= rand.Next(1,level)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_heal *= rand.Next(0,level)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_mana *= rand.Next(0,level)+rndCof;
					
					//null another
					lv1_stat_atck = 0;
					lv1_stat_dmg = 0;
					
					
					
					clType = 5;
					
					selectedName = itemNames[3];
					selectedDescr = itemDescr[3];
					break;
				case 4:
					GraphicsID = 100007;
					rndCof = rand.Next(0,9);
					lv1_stat_def *= rand.Next(1,level)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_heal *= rand.Next(0,level)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_mana *= rand.Next(0,level)+rndCof;
					
					//null another
					lv1_stat_atck = 0;
					lv1_stat_dmg = 0;
					
					
					
					clType = 6;
					
					selectedName = itemNames[4];
					selectedDescr = itemDescr[4];
					break;
				case 5:
					clType = 4;
					GraphicsID = 100007;
					rndCof = rand.Next(0,9);
					lv1_stat_def *= rand.Next(1,level)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_heal *= rand.Next(0,level)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_mana *= rand.Next(0,level)+rndCof;
					
					//null another
					lv1_stat_atck = 0;
					lv1_stat_dmg = 0;
					
					
					
					
					selectedName = itemNames[5];
					selectedDescr = itemDescr[5];
					break;
				case 6:
					clType = 2;
					GraphicsID = 100007;
					rndCof = rand.Next(0,9);
					lv1_stat_def *= rand.Next(1,level)+rndCof;
					rndCof = rand.Next(0,9);
					
					//null another
					lv1_stat_heal = 0;
					lv1_stat_mana = 0;
					lv1_stat_atck = 0;
					lv1_stat_dmg = 0;
					
					
					
					
					
					selectedName = itemNames[6];
					selectedDescr = itemDescr[6];
					break;
			}
			
			//now time to generate Graphics x & Graphics y
			int GraphicsX = 0;
			int GraphicsY = 0;
			//0 - ближние оружие,1 - дальнее оружие,2 - нагрудник,3 - боты,4 - перчи,5 - шлем,6 - щит
			if(type == 0)
			{
				//12 обычного(включая дальнее!!!)
				//8 ближнего,4 дальнего
				int gen = rand.Next(1,8);
				switch(gen)
				{
					case 1:
						GraphicsX = 0;
						GraphicsY = 90;
						break;
					case 2:
						GraphicsX = 0;
						GraphicsY = 45;
						break;
					case 3:
						GraphicsX = 0;
						GraphicsY = 105;
						break;
					case 4:
						GraphicsX = 0;
						GraphicsY = 30;
						break;
					case 5:
						GraphicsX = 0;
						GraphicsY = 15;
						break;
					case 6:
						GraphicsX = 15;
						GraphicsY = 0;
						break;
					case 7:
						GraphicsX = 15;
						GraphicsY = 75;
						break;
					case 8:
						GraphicsX = 0;
						GraphicsY = 0;
						break;
				}
			}
			else if(type == 1)
			{
				//12 обычного(включая дальнее!!!)
				//8 ближнего,4 дальнего
				int gen = rand.Next(1,4);
				switch(gen)
				{
					case 1:
						GraphicsX = 30;
						GraphicsY = 45;
						break;
					case 2:
						GraphicsX = 30;
						GraphicsY = 60;
						break;
					case 3:
						GraphicsX = 30;
						GraphicsY = 75;
						break;
					case 4:
						GraphicsX = 30;
						GraphicsY = 105;
						break;
				}
			}
			else if(type == 2)
			{
				//1
				int gen = rand.Next(1,4);
				switch(gen)
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
						GraphicsY = 105;
						break;
					case 4:
						GraphicsX = 15;
						GraphicsY = 0;
						break;
				}
			}
			else if(type == 3)
			{
				GraphicsX = 15;
				GraphicsY = 15;
			}
			else if(type == 4)
			{
				//1
				int gen = rand.Next(1,2);
				switch(gen)
				{
					case 1:
						GraphicsX = 15;
						GraphicsY = 60;
						break;
					case 2:
						GraphicsX = 15;
						GraphicsY = 75;
						break;
				}
			}
			else if(type == 5)
			{
				//1
				int gen = rand.Next(1,2);
				switch(gen)
				{
					case 1:
						GraphicsX = 0;
						GraphicsY = 0;
						break;
					case 2:
						GraphicsX = 0;
						GraphicsY = 30;
						break;
				}
			}
			else if(type == 6)
			{
				//1
				int gen = rand.Next(1,3);
				switch(gen)
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
			}
			
			//finally...trying to save item xml
			string release = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Item>
	<type>"+clType+@"</type><!-- client type id: сделать select надо CLIENT_TYPE_UNKNOWN = 0 CLIENT_TYPE_WEAPON_1 = 1 CLIENT_TYPE_SHIELD_1 = 2 CLIENT_TYPE_ARMOR = 3 CLIENT_TYPE_HELMET = 4 CLIENT_TYPE_BOOTS = 5 CLIENT_TYPE_GLOVES = 6   ВСЕ ЗНАЧЕНИЯ НА РУССКИЙ ПЕРЕВЕДИ-->
	<set_id>0</set_id>
	<graphics_id>"+GraphicsID+@"</graphics_id>
	<graphics_x>"+GraphicsX+@"</graphics_x>
	<graphics_y>"+GraphicsY+@"</graphics_y>
	<name>"+selectedName+@"</name><!-- имя -->
	<description>"+selectedDescr+@"</description><!-- описание -->
	<available_status>all</available_status><!-- ничё не трогать как было так и оставить -->
	<can_sell>1</can_sell><!-- да-1,нет-0-->
	<can_drop>1</can_drop><!-- да-1,нет-0-->
	<max_units>1</max_units><!-- макс вместимость в один слот -->
	<price>"+gold+@"</price><!-- стоимость -->
	<health_effect>"+lv1_stat_heal+@"</health_effect>
	<mana_effect>"+lv1_stat_mana+@"</mana_effect>
	<attack_effect>"+lv1_stat_atck+@"</attack_effect>
	<defense_effect>"+lv1_stat_def+@"</defense_effect>
	<damage_effect>"+lv1_stat_dmg+@"</damage_effect>
	<skill_effect>0</skill_effect>
	<magic_effect>0</magic_effect>
	<healthregenerate_effect>0</healthregenerate_effect>
	<manaregenerate_effect>0</manaregenerate_effect>
	<action_effect_1>0</action_effect_1><!-- 1-healing,2-mana healing,3-skill(trigger on target),4-fire wall, mass attackBase, mass heal --><!-- СТАВЬ ТУТ 0 Т.К остальное кроме меня никто не настроит-->
	<action_effect_1_data></action_effect_1_data><!-- data splits by : . for healing data contains only +hp amount,for mana too--><!-- оставь пустым ячейку-->
	<action_effect_2>0</action_effect_2><!-- сдесь 0-->
	<action_effect_2_data></action_effect_2_data><!-- тут пусто-->
	<effect_duration>0</effect_duration>
	<required_skill>0</required_skill>
	<required_magic>0</required_magic>
	<frequency>"+frequency+@"</frequency>
	<range>"+range+@"</range>
	<premium>0</premium>
	<usage_type>1</usage_type><!-- USAGE_TYPE_UNKNOWN = 0 USAGE_TYPE_EQUIP = 1 USAGE_TYPE_USE = 2 USAGE_TYPE_GOLD = 3   ПЕРЕВЕСТИ НА РУССКИЙ!!-->
	<static>false</static>
</Item>
";
			StreamWriter sw=new StreamWriter("Data/Items/Item_"+generatedId+".xml");
			sw.Write(release);
			sw.Flush();
			sw.Close();
			
			
			
			//Random rnd=new Random();
			int objectID=rand.Next(100000,999999);
			int itemID = generatedId;
			Items.Item it=new RhynnServer.Code.Items.Item();
            it.id=generatedId;
            it.ObjectID=objectID;
            it.units=1;
            		
            //int GraphicsID=int.Parse(XmlReader.GetValueFromXml("Data/Items/Item_"+itemID+".xml","/Item/graphics_id"));
			//int GraphicsX=int.Parse(XmlReader.GetValueFromXml("Data/Items/Item_"+itemID+".xml","/Item/graphics_x"));
			//int GraphicsY=int.Parse(XmlReader.GetValueFromXml("Data/Items/Item_"+itemID+".xml","/Item/graphics_y"));
			int usageType=int.Parse(XmlReader.GetValueFromXml("Data/Items/Item_"+itemID+".xml","/Item/usage_type"));
            		
			it.GraphicsID=GraphicsID;
			it.GraphicsX=GraphicsX;
			it.GraphicsY=GraphicsY;
			it.usageType=usageType;
			
			int xp = srand.Next(0,10);
			int yp = srand.Next(0,10);
			
			it.x=who.mobj.X-xp;
			it.y=who.mobj.Y-yp;
						
			Items.ItemCL.AddItemToAll(who.MapID,it);
			
		}
		
		
		
		
		public static void DropItemMedium(Mob who,int level)
		{
			Random rand=new Random();
			int rndCof = rand.Next(1,9);
			int gold = 0;
			int lv1_stat_atck = 2;
			int lv1_stat_dmg = 1;
			int lv1_stat_def = 2;
			int lv1_stat_heal = 3;
			int lv1_stat_mana = 2;
			int frequency = 120;
			int range = 10;//ближнее оружие
			int reqSkill = 16;
			int reqMagic = 16;
			int maxReqSkill = level*reqSkill;
			int maxReqMagic = level*reqMagic;
			
			int reSkill = rand.Next(reqSkill,maxReqSkill);
			int reMagic = rand.Next(reqMagic,maxReqMagic);
			
			int GraphicsID = 100006;
			
			string[] itemNames = 
			{
				"Medium weapon",
				"Medium weapon",
				"Medium armor",
				"Medium boots",
				"Medium gloves",
				"Medium helmet",
				"Medium shield"
			};
			
			string[] itemDescr = 
			{
				"near fighting weapon",
				"far fighting weapon",
				"not strong armor",
				"not strong boots",
				"not strong gloves",
				"not strong helmet",
				"not strong shield"
			};
			
			string selectedName = "";
			string selectedDescr = "";
			int generatedId = rand.Next(100000,999999);
			
			int clType = 0;
			
			//generate item
			//0 - ближние оружие,1 - дальнее оружие,2 - нагрудник,3 - боты,4 - перчи,5 - шлем,6 - щит
			int type = rand.Next(0,6);
			gold = level * rand.Next(1,100);
			switch(type)
			{
				case 0:
					clType = 1;
					rndCof = rand.Next(1,9);
					lv1_stat_atck *= rand.Next(level/2,level+level/2)+rndCof;
					rndCof = rand.Next(1,9);
					lv1_stat_dmg *= rand.Next(level/2,level+level/2)+rndCof;
					rndCof = rand.Next(1,level/2);
					frequency +=rndCof;
					
					//null another
					lv1_stat_mana = 0;
					lv1_stat_heal = 0;
					lv1_stat_def = 0;
					
					
					selectedName = itemNames[0];
					selectedDescr = itemDescr[0];
					break;
				case 1:
					clType = 1;
					rndCof = rand.Next(1,9);
					lv1_stat_atck *= rand.Next(level/2,level+level/2)+rndCof;
					rndCof = rand.Next(1,9);
					lv1_stat_dmg *= rand.Next(level/2,level+level/2)+rndCof;
					rndCof = rand.Next(1,9);
					range = 50 + rndCof;
					
					//null another
					lv1_stat_mana = 0;
					lv1_stat_heal = 0;
					lv1_stat_def = 0;					
					
					
					
					
					selectedName = itemNames[1];
					selectedDescr = itemDescr[1];
					break;
				case 2:
					clType = 3;
					GraphicsID = 100007;
					rndCof = rand.Next(0,9);
					lv1_stat_def *= rand.Next(level,level+level/2)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_heal *= rand.Next(level/2,level)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_mana *= rand.Next(level/2,level)+rndCof;
					
					//null another
					lv1_stat_atck = 0;
					lv1_stat_dmg = 0;
					
					
					
					
					selectedName = itemNames[2];
					selectedDescr = itemDescr[2];
					break;
				case 3:
					GraphicsID = 100007;
					rndCof = rand.Next(0,9);
					lv1_stat_def *= rand.Next(level/2,level+level/2)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_heal *= rand.Next(level/2,level)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_mana *= rand.Next(level/2,level)+rndCof;
					
					//null another
					lv1_stat_atck = 0;
					lv1_stat_dmg = 0;
					
					
					
					clType = 5;
					
					selectedName = itemNames[3];
					selectedDescr = itemDescr[3];
					break;
				case 4:
					GraphicsID = 100007;
					rndCof = rand.Next(0,9);
					lv1_stat_def *= rand.Next(level/2,level+level/2)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_heal *= rand.Next(level/2,level)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_mana *= rand.Next(level/2,level)+rndCof;
					
					//null another
					lv1_stat_atck = 0;
					lv1_stat_dmg = 0;
					
					
					
					clType = 6;
					
					selectedName = itemNames[4];
					selectedDescr = itemDescr[4];
					break;
				case 5:
					clType = 4;
					GraphicsID = 100007;
					rndCof = rand.Next(0,9);
					lv1_stat_def *= rand.Next(level/2,level+level/2)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_heal *= rand.Next(level/2,level)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_mana *= rand.Next(level/2,level)+rndCof;
					
					//null another
					lv1_stat_atck = 0;
					lv1_stat_dmg = 0;
					
					
					
					
					selectedName = itemNames[5];
					selectedDescr = itemDescr[5];
					break;
				case 6:
					clType = 2;
					GraphicsID = 100007;
					rndCof = rand.Next(0,9);
					lv1_stat_def *= rand.Next(level/2,level+level/2)+rndCof;
					rndCof = rand.Next(0,9);
					
					//null another
					lv1_stat_heal = 0;
					lv1_stat_mana = 0;
					lv1_stat_atck = 0;
					lv1_stat_dmg = 0;
					
					
					
					
					
					selectedName = itemNames[6];
					selectedDescr = itemDescr[6];
					break;
			}
			
			//now time to generate Graphics x & Graphics y
			int GraphicsX = 0;
			int GraphicsY = 0;
			//0 - ближние оружие,1 - дальнее оружие,2 - нагрудник,3 - боты,4 - перчи,5 - шлем,6 - щит
			if(type == 0)
			{
				//12 обычного(включая дальнее!!!)
				//8 ближнего,4 дальнего
				int gen = rand.Next(1,8);
				switch(gen)
				{
					case 1:
						GraphicsX = 0;
						GraphicsY = 90;
						break;
					case 2:
						GraphicsX = 0;
						GraphicsY = 45;
						break;
					case 3:
						GraphicsX = 0;
						GraphicsY = 105;
						break;
					case 4:
						GraphicsX = 0;
						GraphicsY = 30;
						break;
					case 5:
						GraphicsX = 0;
						GraphicsY = 15;
						break;
					case 6:
						GraphicsX = 15;
						GraphicsY = 0;
						break;
					case 7:
						GraphicsX = 15;
						GraphicsY = 75;
						break;
					case 8:
						GraphicsX = 0;
						GraphicsY = 0;
						break;
				}
			}
			else if(type == 1)
			{
				//12 обычного(включая дальнее!!!)
				//8 ближнего,4 дальнего
				int gen = rand.Next(1,4);
				switch(gen)
				{
					case 1:
						GraphicsX = 30;
						GraphicsY = 45;
						break;
					case 2:
						GraphicsX = 30;
						GraphicsY = 60;
						break;
					case 3:
						GraphicsX = 30;
						GraphicsY = 75;
						break;
					case 4:
						GraphicsX = 30;
						GraphicsY = 105;
						break;
				}
			}
			else if(type == 2)
			{
				//1
				int gen = rand.Next(1,4);
				switch(gen)
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
						GraphicsY = 105;
						break;
					case 4:
						GraphicsX = 15;
						GraphicsY = 0;
						break;
				}
			}
			else if(type == 3)
			{
				GraphicsX = 15;
				GraphicsY = 15;
			}
			else if(type == 4)
			{
				//1
				int gen = rand.Next(1,2);
				switch(gen)
				{
					case 1:
						GraphicsX = 15;
						GraphicsY = 60;
						break;
					case 2:
						GraphicsX = 15;
						GraphicsY = 75;
						break;
				}
			}
			else if(type == 5)
			{
				//1
				int gen = rand.Next(1,2);
				switch(gen)
				{
					case 1:
						GraphicsX = 0;
						GraphicsY = 0;
						break;
					case 2:
						GraphicsX = 0;
						GraphicsY = 30;
						break;
				}
			}
			else if(type == 6)
			{
				//1
				int gen = rand.Next(1,3);
				switch(gen)
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
			}
			
			//finally...trying to save item xml
			string release = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Item>
	<type>"+clType+@"</type><!-- client type id: сделать select надо CLIENT_TYPE_UNKNOWN = 0 CLIENT_TYPE_WEAPON_1 = 1 CLIENT_TYPE_SHIELD_1 = 2 CLIENT_TYPE_ARMOR = 3 CLIENT_TYPE_HELMET = 4 CLIENT_TYPE_BOOTS = 5 CLIENT_TYPE_GLOVES = 6   ВСЕ ЗНАЧЕНИЯ НА РУССКИЙ ПЕРЕВЕДИ-->
	<set_id>0</set_id>
	<graphics_id>"+GraphicsID+@"</graphics_id>
	<graphics_x>"+GraphicsX+@"</graphics_x>
	<graphics_y>"+GraphicsY+@"</graphics_y>
	<name>"+selectedName+@"</name><!-- имя -->
	<description>"+selectedDescr+@"</description><!-- описание -->
	<available_status>all</available_status><!-- ничё не трогать как было так и оставить -->
	<can_sell>1</can_sell><!-- да-1,нет-0-->
	<can_drop>1</can_drop><!-- да-1,нет-0-->
	<max_units>1</max_units><!-- макс вместимость в один слот -->
	<price>"+gold+@"</price><!-- стоимость -->
	<health_effect>"+lv1_stat_heal+@"</health_effect>
	<mana_effect>"+lv1_stat_mana+@"</mana_effect>
	<attack_effect>"+lv1_stat_atck+@"</attack_effect>
	<defense_effect>"+lv1_stat_def+@"</defense_effect>
	<damage_effect>"+lv1_stat_dmg+@"</damage_effect>
	<skill_effect>0</skill_effect>
	<magic_effect>0</magic_effect>
	<healthregenerate_effect>0</healthregenerate_effect>
	<manaregenerate_effect>0</manaregenerate_effect>
	<action_effect_1>0</action_effect_1><!-- 1-healing,2-mana healing,3-skill(trigger on target),4-fire wall, mass attackBase, mass heal --><!-- СТАВЬ ТУТ 0 Т.К остальное кроме меня никто не настроит-->
	<action_effect_1_data></action_effect_1_data><!-- data splits by : . for healing data contains only +hp amount,for mana too--><!-- оставь пустым ячейку-->
	<action_effect_2>0</action_effect_2><!-- сдесь 0-->
	<action_effect_2_data></action_effect_2_data><!-- тут пусто-->
	<effect_duration>0</effect_duration>
	<required_skill>"+reSkill+@"</required_skill>
	<required_magic>"+reMagic+@"</required_magic>
	<frequency>"+frequency+@"</frequency>
	<range>"+range+@"</range>
	<premium>0</premium>
	<usage_type>1</usage_type><!-- USAGE_TYPE_UNKNOWN = 0 USAGE_TYPE_EQUIP = 1 USAGE_TYPE_USE = 2 USAGE_TYPE_GOLD = 3   ПЕРЕВЕСТИ НА РУССКИЙ!!-->
	<static>false</static>
</Item>
";
			StreamWriter sw=new StreamWriter("Data/Items/Item_"+generatedId+".xml");
			sw.Write(release);
			sw.Flush();
			sw.Close();
			
			
			
			//Random rnd=new Random();
			int objectID=rand.Next(100000,999999);
			int itemID = generatedId;
			Items.Item it=new RhynnServer.Code.Items.Item();
            it.id=generatedId;
            it.ObjectID=objectID;
            it.units=1;
            		
            //int GraphicsID=int.Parse(XmlReader.GetValueFromXml("Data/Items/Item_"+itemID+".xml","/Item/graphics_id"));
			//int GraphicsX=int.Parse(XmlReader.GetValueFromXml("Data/Items/Item_"+itemID+".xml","/Item/graphics_x"));
			//int GraphicsY=int.Parse(XmlReader.GetValueFromXml("Data/Items/Item_"+itemID+".xml","/Item/graphics_y"));
			int usageType=int.Parse(XmlReader.GetValueFromXml("Data/Items/Item_"+itemID+".xml","/Item/usage_type"));
            		
			it.GraphicsID=GraphicsID;
			it.GraphicsX=GraphicsX;
			it.GraphicsY=GraphicsY;
			it.usageType=usageType;
			int xp = srand.Next(0,10);
			int yp = srand.Next(0,10);
			
			it.x=who.mobj.X-xp;
			it.y=who.mobj.Y-yp;
						
			Items.ItemCL.AddItemToAll(who.MapID,it);
			
		}
		
		
		
		public static void DropItemLegendary(Mob who,int level)
		{
			Random rand=new Random();
			int rndCof = rand.Next(1,9);
			int gold = 0;
			int lv1_stat_atck = 2;
			int lv1_stat_dmg = 1;
			int lv1_stat_def = 2;
			int lv1_stat_heal = 3;
			int lv1_stat_mana = 2;
			int frequency = 120;
			int range = 10;//ближнее оружие
			int reqSkill = 16;
			int reqMagic = 16;
			int maxReqSkill = level*reqSkill;
			int maxReqMagic = level*reqMagic;
			
			int reSkill = rand.Next(reqSkill,maxReqSkill);
			int reMagic = rand.Next(reqMagic,maxReqMagic);
			
			int GraphicsID = 100006;
			
			string[] itemNames = 
			{
				"Medium weapon",
				"Medium weapon",
				"Medium armor",
				"Medium boots",
				"Medium gloves",
				"Medium helmet",
				"Medium shield"
			};
			
			string[] itemDescr = 
			{
				"near fighting weapon",
				"far fighting weapon",
				"not strong armor",
				"not strong boots",
				"not strong gloves",
				"not strong helmet",
				"not strong shield"
			};
			
			string selectedName = "";
			string selectedDescr = "";
			int generatedId = rand.Next(100000,999999);
			
			int clType = 0;
			
			//generate item
			//0 - ближние оружие,1 - дальнее оружие,2 - нагрудник,3 - боты,4 - перчи,5 - шлем,6 - щит
			int type = rand.Next(0,6);
			gold = level * rand.Next(1,100);
			switch(type)
			{
				case 0:
					clType = 1;
					rndCof = rand.Next(1,9);
					lv1_stat_atck *= rand.Next(level,level+level/2)+rndCof;
					rndCof = rand.Next(1,9);
					lv1_stat_dmg *= rand.Next(level,level+level/2)+rndCof;
					rndCof = rand.Next(level/2,level);
					frequency +=rndCof;
					
					//null another
					lv1_stat_mana = 0;
					lv1_stat_heal = 0;
					lv1_stat_def = 0;
					
					
					selectedName = itemNames[0];
					selectedDescr = itemDescr[0];
					break;
				case 1:
					clType = 1;
					rndCof = rand.Next(1,9);
					lv1_stat_atck *= rand.Next(level,level+level/2)+rndCof;
					rndCof = rand.Next(1,9);
					lv1_stat_dmg *= rand.Next(level,level+level/2)+rndCof;
					rndCof = rand.Next(1,9);
					range = 20 + rndCof;
					
					//null another
					lv1_stat_mana = 0;
					lv1_stat_heal = 0;
					lv1_stat_def = 0;					
					
					
					
					
					selectedName = itemNames[1];
					selectedDescr = itemDescr[1];
					break;
				case 2:
					clType = 3;
					GraphicsID = 100007;
					rndCof = rand.Next(0,9);
					lv1_stat_def *= rand.Next(level,level+level/2)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_heal *= rand.Next(level,level+level/2)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_mana *= rand.Next(level,level+level/2)+rndCof;
					
					//null another
					lv1_stat_atck = 0;
					lv1_stat_dmg = 0;
					
					
					
					
					selectedName = itemNames[2];
					selectedDescr = itemDescr[2];
					break;
				case 3:
					GraphicsID = 100007;
					rndCof = rand.Next(0,9);
					lv1_stat_def *= rand.Next(level,level+level/2)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_heal *= rand.Next(level,level+level/2)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_mana *= rand.Next(level,level+level/2)+rndCof;
					
					//null another
					lv1_stat_atck = 0;
					lv1_stat_dmg = 0;
					
					
					
					clType = 5;
					
					selectedName = itemNames[3];
					selectedDescr = itemDescr[3];
					break;
				case 4:
					GraphicsID = 100007;
					rndCof = rand.Next(0,9);
					lv1_stat_def *= rand.Next(level,level+level/2)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_heal *= rand.Next(level,level+level/2)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_mana *= rand.Next(level,level+level/2)+rndCof;
					
					//null another
					lv1_stat_atck = 0;
					lv1_stat_dmg = 0;
					
					
					
					clType = 6;
					
					selectedName = itemNames[4];
					selectedDescr = itemDescr[4];
					break;
				case 5:
					clType = 4;
					GraphicsID = 100007;
					rndCof = rand.Next(0,9);
					lv1_stat_def *= rand.Next(level,level+level/2)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_heal *= rand.Next(level,level+level/2)+rndCof;
					rndCof = rand.Next(0,9);
					lv1_stat_mana *= rand.Next(level,level+level/2)+rndCof;
					
					//null another
					lv1_stat_atck = 0;
					lv1_stat_dmg = 0;
					
					
					
					
					selectedName = itemNames[5];
					selectedDescr = itemDescr[5];
					break;
				case 6:
					clType = 2;
					GraphicsID = 100007;
					rndCof = rand.Next(0,9);
					lv1_stat_def *= rand.Next(level,level+level/2)+rndCof;
					rndCof = rand.Next(0,9);
					
					//null another
					lv1_stat_heal = 0;
					lv1_stat_mana = 0;
					lv1_stat_atck = 0;
					lv1_stat_dmg = 0;
					
					
					
					
					
					selectedName = itemNames[6];
					selectedDescr = itemDescr[6];
					break;
			}
			
			//now time to generate Graphics x & Graphics y
			int GraphicsX = 0;
			int GraphicsY = 0;
			//0 - ближние оружие,1 - дальнее оружие,2 - нагрудник,3 - боты,4 - перчи,5 - шлем,6 - щит
			if(type == 0)
			{
				//12 ближнего
				int gen = rand.Next(1,12);
				switch(gen)
				{
					case 1:
						GraphicsX = 15;
						GraphicsY = 45;
						break;
					case 2:
						GraphicsX = 0;
						GraphicsY = 75;
						break;
					case 3:
						GraphicsX = 15;
						GraphicsY = 90;
						break;
					case 4:
						GraphicsX = 45;
						GraphicsY = 0;
						break;
					case 5:
						GraphicsX = 15;
						GraphicsY = 30;
						break;
					case 6:
						GraphicsX = 30;
						GraphicsY = 30;
						break;
					case 7:
						GraphicsX = 30;
						GraphicsY = 15;
						break;
					case 8:
						GraphicsX = 30;
						GraphicsY = 0;
						break;
					case 9:
						GraphicsX = 15;
						GraphicsY = 15;
						break;
					case 10:
						GraphicsX = 0;
						GraphicsY = 60;
						break;
					case 11:
						GraphicsX = 15;
						GraphicsY = 105;
						break;
					case 12:
						GraphicsX = 15;
						GraphicsY = 60;
						break;
				}
			}
			else if(type == 1)
			{
				GraphicsX = 30;
				GraphicsY = 75;
			}
			else if(type == 2)
			{
				GraphicsX = 0;
				GraphicsY = 90;
			}
			else if(type == 3)
			{
				int gen = rand.Next(1,2);
				switch(gen)
				{
					case 1:
						GraphicsX = 15;
						GraphicsY = 30;
						break;
					case 2:
						GraphicsX = 15;
						GraphicsY = 45;
						break;
				}
			}
			else if(type == 4)
			{
				GraphicsX = 15;
				GraphicsY = 90;
			}
			else if(type == 5)
			{
				//1
				int gen = rand.Next(1,2);
				switch(gen)
				{
					case 1:
						GraphicsX = 0;
						GraphicsY = 15;
						break;
					case 2:
						GraphicsX = 0;
						GraphicsY = 45;
						break;
				}
			}
			
			//finally...trying to save item xml
			string release = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Item>
	<type>"+clType+@"</type><!-- client type id: сделать select надо CLIENT_TYPE_UNKNOWN = 0 CLIENT_TYPE_WEAPON_1 = 1 CLIENT_TYPE_SHIELD_1 = 2 CLIENT_TYPE_ARMOR = 3 CLIENT_TYPE_HELMET = 4 CLIENT_TYPE_BOOTS = 5 CLIENT_TYPE_GLOVES = 6   ВСЕ ЗНАЧЕНИЯ НА РУССКИЙ ПЕРЕВЕДИ-->
	<set_id>0</set_id>
	<graphics_id>"+GraphicsID+@"</graphics_id>
	<graphics_x>"+GraphicsX+@"</graphics_x>
	<graphics_y>"+GraphicsY+@"</graphics_y>
	<name>"+selectedName+@"</name><!-- имя -->
	<description>"+selectedDescr+@"</description><!-- описание -->
	<available_status>all</available_status><!-- ничё не трогать как было так и оставить -->
	<can_sell>1</can_sell><!-- да-1,нет-0-->
	<can_drop>1</can_drop><!-- да-1,нет-0-->
	<max_units>1</max_units><!-- макс вместимость в один слот -->
	<price>"+gold+@"</price><!-- стоимость -->
	<health_effect>"+lv1_stat_heal+@"</health_effect>
	<mana_effect>"+lv1_stat_mana+@"</mana_effect>
	<attack_effect>"+lv1_stat_atck+@"</attack_effect>
	<defense_effect>"+lv1_stat_def+@"</defense_effect>
	<damage_effect>"+lv1_stat_dmg+@"</damage_effect>
	<skill_effect>0</skill_effect>
	<magic_effect>0</magic_effect>
	<healthregenerate_effect>0</healthregenerate_effect>
	<manaregenerate_effect>0</manaregenerate_effect>
	<action_effect_1>0</action_effect_1><!-- 1-healing,2-mana healing,3-skill(trigger on target),4-fire wall, mass attackBase, mass heal --><!-- СТАВЬ ТУТ 0 Т.К остальное кроме меня никто не настроит-->
	<action_effect_1_data></action_effect_1_data><!-- data splits by : . for healing data contains only +hp amount,for mana too--><!-- оставь пустым ячейку-->
	<action_effect_2>0</action_effect_2><!-- сдесь 0-->
	<action_effect_2_data></action_effect_2_data><!-- тут пусто-->
	<effect_duration>0</effect_duration>
	<required_skill>"+reSkill+@"</required_skill>
	<required_magic>"+reMagic+@"</required_magic>
	<frequency>"+frequency+@"</frequency>
	<range>"+range+@"</range>
	<premium>0</premium>
	<usage_type>1</usage_type><!-- USAGE_TYPE_UNKNOWN = 0 USAGE_TYPE_EQUIP = 1 USAGE_TYPE_USE = 2 USAGE_TYPE_GOLD = 3   ПЕРЕВЕСТИ НА РУССКИЙ!!-->
	<static>false</static>
</Item>
";
			StreamWriter sw=new StreamWriter("Data/Items/Item_"+generatedId+".xml");
			sw.Write(release);
			sw.Flush();
			sw.Close();
			
			
			
			//Random rnd=new Random();
			int objectID=rand.Next(100000,999999);
			int itemID = generatedId;
			Items.Item it=new RhynnServer.Code.Items.Item();
            it.id=generatedId;
            it.ObjectID=objectID;
            it.units=1;
            		
            //int GraphicsID=int.Parse(XmlReader.GetValueFromXml("Data/Items/Item_"+itemID+".xml","/Item/graphics_id"));
			//int GraphicsX=int.Parse(XmlReader.GetValueFromXml("Data/Items/Item_"+itemID+".xml","/Item/graphics_x"));
			//int GraphicsY=int.Parse(XmlReader.GetValueFromXml("Data/Items/Item_"+itemID+".xml","/Item/graphics_y"));
			int usageType=int.Parse(XmlReader.GetValueFromXml("Data/Items/Item_"+itemID+".xml","/Item/usage_type"));
            		
			it.GraphicsID=GraphicsID;
			it.GraphicsX=GraphicsX;
			it.GraphicsY=GraphicsY;
			it.usageType=usageType;
			int xp = srand.Next(0,10);
			int yp = srand.Next(0,10);
			
			it.x=who.mobj.X-xp;
			it.y=who.mobj.Y-yp;
						
			Items.ItemCL.AddItemToAll(who.MapID,it);
			
		}
		
		public static void DropPotion(Mob who)
		{
			int itemID = 6;
			
			Random rand=new Random();
			int what = rand.Next(0,1);
			if(what==0)
			{
				itemID = 6;
			}
			else
			{
				itemID = 9;
			}
			
			Random rnd=new Random();
			int objectID=rnd.Next(100000,999999);
			Items.Item it=new RhynnServer.Code.Items.Item();
            it.id=itemID;
            it.ObjectID=objectID;
            it.units=3;
            		
            int GraphicsID=int.Parse(XmlReader.GetValueFromXml("Data/Items/Item_"+itemID+".xml","/Item/graphics_id"));
			int GraphicsX=int.Parse(XmlReader.GetValueFromXml("Data/Items/Item_"+itemID+".xml","/Item/graphics_x"));
			int GraphicsY=int.Parse(XmlReader.GetValueFromXml("Data/Items/Item_"+itemID+".xml","/Item/graphics_y"));
			int usageType=int.Parse(XmlReader.GetValueFromXml("Data/Items/Item_"+itemID+".xml","/Item/usage_type"));
            		
			it.GraphicsID=GraphicsID;
			it.GraphicsX=GraphicsX;
			it.GraphicsY=GraphicsY;
			it.usageType=usageType;
			int xp = srand.Next(0,10);
			int yp = srand.Next(0,10);
			
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
			int xp = srand.Next(0,10);
			int yp = srand.Next(0,10);
			
			it.x=who.mobj.X-xp;
			it.y=who.mobj.Y-yp;
						
			Items.ItemCL.AddItemToAll(who.MapID,it);
		}
	}
}
