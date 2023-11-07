export interface Asegurado {
    id:       number;
    cedula:   string;
    nombre:   string;
    telefono: string;
    edad:     number;
    seguros:  SeguroElement[];
}

export interface SeguroElement {
    id:              number;
    nombreSeguro:    string;
    codigoSeguro:    number;
    sumadaAsegurada: number;
    prima:           number;
    asegurados:      Array<PurpleAsegurado | null>;
}

export interface PurpleAsegurado {
    id:       number;
    cedula:   string;
    nombre:   string;
    telefono: string;
    edad:     number;
    seguros:  Array<AseguradoSeguro | null>;
}

export interface AseguradoSeguro {
    id:              number;
    nombreSeguro:    string;
    codigoSeguro:    number;
    sumadaAsegurada: number;
    prima:           number;
    asegurados:      Array<FluffyAsegurado | null>;
}

export interface FluffyAsegurado {
    id:       number;
    cedula:   string;
    nombre:   string;
    telefono: string;
    edad:     number;
    seguros:  null[];
}
