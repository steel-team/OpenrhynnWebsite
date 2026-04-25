/*
 * Created by STeeL
 * 2013
 * Please read file Readme.txt before use this source file
 * 
 * (C) STeeL-Team 2013
 */
using System;
using RhynnServer.Code.Network;
using RhynnServer.Code.Network.Misc;

namespace RhynnServer.Code.Player
{
	/// <summary>
	/// Description of OnDeath.
	/// </summary>
	public class OnDeath
	{
		public static void ProcessDeath(ClientObject obj,int sendto)
		{
			try
			{				
				if(sendto<100000)
            	{
					int h100p = Cache.CacheStorage.GetCharById(obj.SelectedCharID).health_base;
	            	int m100p = Cache.CacheStorage.GetCharById(obj.SelectedCharID).mana_base;
	            	
	            	
	            	int h1p = h100p/100;
	            	int m1p = m100p/100;
	            	
	            	int h70p = h1p * 70;
	            	int m70p = m1p * 70;
	            	
	            	obj.sql.Request("UPDATE characters SET health_current='"+h70p+"',mana_current='"+m70p+"' WHERE id='"+obj.SelectedCharID+"';",0);
	            	
	            	Cache.CacheStorage.GetCharById(sendto).health_current = h70p;
	            	Cache.CacheStorage.GetCharById(sendto).mana_current = m70p;
	            	
            		try
            		{
            			int curx = Cache.CacheStorage.GetCharById(sendto).x;
            			int cury = Cache.CacheStorage.GetCharById(sendto).y;
            			int prev = -1;
            			int savedDestX = 0;
            			int savedDestY = 0;
            			
            			int howmany = obj.nowMap.PortalDestId.Length;
            			for(int i=0;i<howmany;i++)
            			{
            				int px = obj.nowMap.PortalCellsX[i];
            				int py = obj.nowMap.PortalCellsY[i];
            				int x = px*MapFormat.Cell.defaultWidth;
            				int y = py*MapFormat.Cell.defaultHeight;
            				
            				int destx = Math.Abs(x - curx);
            				int desty = Math.Abs(y - cury);
            				
            				int all = destx + desty;
            				if(prev<0)
            				{
            					prev = all;
            					savedDestX = x;
            					savedDestY = y;
            				}
            				else
            				{
            					if(all<prev)
            					{
            						prev = all;
            						savedDestX = x;
	            					savedDestY = y;
            					}
            				}
            			}
            			
            			if(prev>-1)
            			{
            				//ok,nearest portal finded,now try to find cordinates
            				int sdx = savedDestX;
            				int sdy = savedDestY;
            				int _do = 0;//0 - up,1 - right,2 - down,3 -left
            				bool finded = false;
            				int maxTry = 40;//4 origin
            				int currentTry = 0;
            				while(currentTry<maxTry)
            				{
            					switch(_do)
            					{
            						case 0:
            							sdy = savedDestY - 25;//or 27  or 33 or 29(25)(was 27)
            							break;
            						case 1:
            							sdx = savedDestX + 25;
            							break;
            						case 2:
            							sdy = savedDestY + 25;
            							break;
            						case 3:
            							sdx = savedDestX - 25;
            							break;
            					}
            					//Console.WriteLine(sdx+","+sdy);
            					
            					int s1x = 0;
            					int s1y = 0;
            					bool f1 = false;
            					
            					int s2x = 0;
            					int s2y = 0;
            					bool f2 = false;
            					
            					int s3x = 0;
            					int s3y = 0;
            					bool f3 = false;
            					
            					int s4x = 0;
            					int s4y = 0;
            					bool f4 = false;
            					
            					int s5x = 0;
            					int s5y = 0;
            					bool f5 = false;
            					
            					if(!MapFormat.CellTools.cellAt(sdx,sdy,obj.nowMap).IsBlocked())
            					{
            						s1x = sdx;
            						s1y = sdy;
            						finded = true;
            						f1 = true;
            						//break;
            					}
            					if(!MapFormat.CellTools.cellAt(sdx,sdy-25,obj.nowMap).IsBlocked())
            					{
            						s2x = sdx - 25;
            						s2y = sdy - 25;
            						finded = true;
            						f2 = true;
            						//break;
            					}
            					if(!MapFormat.CellTools.cellAt(sdx,sdy+25,obj.nowMap).IsBlocked())
            					{
            						s3x = sdx;
            						s3y = sdy + 25;
            						finded = true;
            						f3 = true;
            						//break;
            					}
            					else if(!MapFormat.CellTools.cellAt(sdx-25,sdy,obj.nowMap).IsBlocked())
            					{
            						s4x = sdx - 25;
            						s4y = sdy;
            						finded = true;
            						f4 = true;
            						//break;
            					}
            					else if(!MapFormat.CellTools.cellAt(sdx+25,sdy,obj.nowMap).IsBlocked())
            					{
            						s5x = sdx + 25;            						
            						s5y = sdy;
            						finded = true;
            						f5 = true;
            						//break;
            					}
            					if(finded)
            					{
            						int all=savedDestX + savedDestY;
            						int a1 = s1x+s1y;
            						int a2 = s2x+s2y;
            						int a3 = s3x+s3y;
            						int a4 = s4x+s4y;
            						int a5 = s5x+s5y;
            						
            						if(a1>a2 && a1>a3 && a1> a4 && a1>a5)
            						{
            							if(f1)
            							{
            								sdx = s1x;
            								sdy = s1y;
            								break;
            							}
            						}
            						else if(a2>a1 && a1>a3 && a1> a4 && a1>a5)
            						{
            							if(f2)
            							{
            								sdx = s2x;
            								sdy = s2y;
            								break;
            							}
            						}
            						else if(a3>a2 && a3>a1 && a1> a4 && a1>a5)
            						{
            							if(f3)
            							{
            								sdx = s3x;
            								sdy = s3y;
            								break;
            							}
            						}
            						else if(a4>a2 && a4>a3 && a4> a1 && a1>a5)
            						{
            							if(f4)
            							{
            								sdx = s4x;
            								sdy = s4y;
            								break;
            							}
            						}
            						else if(a5>a2 && a5>a3 && a5> a4 && a5>a1)
            						{
            							if(f5)
            							{
            								sdx = s5x;
            								sdy = s5y;
            								break;
            							}
            						}
            					}
            					if(!finded)
            					{
            						switch(_do)
            						{
            							case 0:
            								sdy = savedDestY + 27;//or 27  or 33          						
            								break;
            							case 1:
            								sdx = savedDestX - 27;
            								break;
            							case 2:
            								sdy = savedDestY - 27;
            								break;
            							case 3:
            								sdx = savedDestX + 27;
            								break;
            						}
            					}
            					_do++;
            					currentTry++;
            				}
            				if(finded)
            				{
            					//obj.sql.Request("UPDATE characters SET x='"+sdx+"',y='"+sdy+"' WHERE id='"+sendto+"';",0);
            					Cache.CacheStorage.GetCharById(sendto).x = sdx;
            					Cache.CacheStorage.GetCharById(sendto).y = sdy;
            				}
            			}
            		}	
            		catch(Exception ex)
            		{
            			ServConsole.Debug("Error in pfxy:"+ex.ToString());
            		}
            	}
			}
			catch(Exception ex2)
			{
				ServConsole.Error("[OnDeath]"+ex2.ToString());
			}
		}
	}
}
