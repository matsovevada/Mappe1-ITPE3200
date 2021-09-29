import { Baater } from "./Baater";
import { Lugar } from "./Lugar";
import { Strekning } from "./Strekning";

export class Avgang {
  id: number;
  strekning: Strekning;
  baat: Baater;
  datoTid: string;
  datoTidTicks: number;
  antallLedigeBilplasser: number;
  ledigeLugarer: Array<Lugar>;
}
