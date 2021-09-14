import { Baater } from "./Baater";
import { Lugar } from "./Lugar";
import { Strekning } from "./Strekning";

export class Avgang {
  id: number;
  strekning: Strekning;
  baat: Baater;
  dato: Date;
  antallLedigeBilplasser: number;
  lugarer: Array<Lugar>;
}
