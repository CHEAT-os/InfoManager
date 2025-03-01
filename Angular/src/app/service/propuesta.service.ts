import { Injectable } from '@angular/core';
import { PropuestaModel } from '../models/propuestaModel';

@Injectable({
  providedIn: 'root'
})
export class PropuestaService {

  readonly baseUrl = 'https://localhost:7000/api/Propuesta';

  constructor() { }

  private getAuthHeaders(): { [key: string]: string } {
    const token = localStorage.getItem('token');
    return {
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    };
  }

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

async getPropuestaById(id: number): Promise<PropuestaModel | undefined> {
  const response = await fetch(`${this.baseUrl}/${id}`, {
    method: 'GET',
    headers: this.getAuthHeaders()
  });
  return (await response.json()) as PropuestaModel | undefined;
}

async getPropuestasUsuario(userId: number): Promise<PropuestaModel[]> {
  const response = await fetch(`${this.baseUrl}/usuario/${userId}`, {
    method: 'GET',
    headers: this.getAuthHeaders()
  });
  return await response.json();
}
}
