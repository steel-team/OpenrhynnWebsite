export interface OnlineResponse {
  characters?: Character[];
  online?: number;
  status?: string;
}

export interface Character {
  id?: number;
  clanId?: number;
  charClass?: number;
  manaCurrent?: number;
  playfieldId?: number;
  level?: number;
  manaMax?: number;
  healthMax?: number;
  name?: string;
  healthCurrent?: number;
  playfieldName?: string;
  userType?: string;
}
