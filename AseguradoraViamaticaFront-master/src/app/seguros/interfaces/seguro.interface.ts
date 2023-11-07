export interface Seguro {
    id:              number;
    nombreSeguro:    string;
    codigoSeguro:    number;
    sumadaAsegurada: number;
    prima:           number;
    asegurados:      AseguradoElement[];
}

export interface AseguradoElement {
    id:       number;
    cedula:   string;
    nombre:   Nombre;
    telefono: string;
    edad:     number;
    seguros:  Array<SeguroClass | null>;
}

export enum Nombre {
    JuanPérez = "Juan Pérez",
    JuanPérez22 = "Juan Pérez22",
}

export interface SeguroClass {
    id:              number;
    nombreSeguro:    string;
    codigoSeguro:    number;
    sumadaAsegurada: number;
    prima:           number;
    asegurados:      Array<SeguroAsegurado | null>;
}

export interface SeguroAsegurado {
    id:       number;
    cedula:   string;
    nombre:   Nombre;
    telefono: string;
    edad:     number;
    seguros:  null[];
}