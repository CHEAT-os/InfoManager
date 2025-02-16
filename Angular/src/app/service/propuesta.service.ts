import { Injectable } from '@angular/core';
import { PropuestaModel } from '../models/propuestaModel';

@Injectable({
  providedIn: 'root'
})
export class PropuestaService {

  readonly baseUrl = 'http://localhost:7000/api/Propuesta';

  constructor() { }

  async postPropuesta(propuesta: PropuestaModel): Promise<PropuestaModel> {
    const response = await fetch(this.baseUrl, {
      method: "POST",
      headers: {
          "Content-Type": "application/json"
      },
      body: JSON.stringify(propuesta)
  });

  return await response.json();
}
}
