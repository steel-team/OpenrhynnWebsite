export interface ServersResponse {
  servers?: Server[];
  version?: Version;
  status?: string;
}

export interface Server {
  address?: string;
  public?: boolean;
  name?: string;
}

export interface Version {
  str?: string;
  obj?: Obj;
}

export interface Obj {
  high?: number;
  lowSub?: number;
  low?: number;
}
