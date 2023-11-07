
export interface AseguradoCreacion {
    cedula: string;
    nombre: string;
    telefono: string;
    edad: number;
    seguros: SeguroElementCreacion[];
}
export interface SeguroElementCreacion {
    seguroId: number;
}

