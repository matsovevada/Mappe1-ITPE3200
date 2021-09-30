import { Lugar } from "./Lugar";

export class Billett {
	id: number;
	avgangId: number;
	avgangIdRetur: number;
	kundeId: number;
	antallPersoner: number;
	antallPersonerRetur: number;
	totalPris: number;
	bilplass: boolean;
	bilplassRetur: boolean;
	lugarer: Array<Lugar>;
	lugarerRetur: Array<Lugar>;
	
}


