import { Baat } from "./Baat";
import { Lugar } from "./Lugar";
import { Strekning } from "./Strekning";

export class Avgang {
  id: number;
  strekning: Strekning;
  baat: Baat;
  datoTid: string;
  datoTidTicks: number;
  antallLedigeBilplasser: number;
  ledigeLugarer: Array<Lugar>;
}
