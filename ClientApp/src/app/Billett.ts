import { Lugar } from "./Lugar";

export class Billett {
	id: number;
	avgangId: number;
	kundeId: number;
	totalPris: number;
	bilplass: boolean;
	lugarer: Array<Lugar>;
}


