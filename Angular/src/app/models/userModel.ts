//Modelo user prueba

import { PropuestaModel } from "./propuestaModel";

export interface userModel{
    id: number;
    name: string;
    apellidos: string;
    userName: string;
    email: string;
    password: string;
    rol: string;
    propuestas: PropuestaModel[];
}