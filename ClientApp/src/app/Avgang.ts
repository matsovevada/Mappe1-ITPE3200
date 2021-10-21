import { Baat } from "./Baat";
import { Lugar } from "./Lugar";

export class Avgang {
  id: number;
  strekningFra: string;
  strekningTil: string;
  baat: Baat;
  datoTid: string;
  datoTidTicks: number;
  antallLedigeBilplasser: number;
  ledigeLugarer: Array<Lugar>;
}
