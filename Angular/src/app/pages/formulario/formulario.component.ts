import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { PropuestaService } from 'src/app/service/propuesta.service';
import { PropuestaModel } from '../../models/propuestaModel';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-formulario',
  imports: [ReactiveFormsModule, CommonModule],
  standalone: true,
  templateUrl: './formulario.component.html',
  styleUrls: ['./formulario.component.css']
})
export class FormularioComponent {
  applyForm = new FormGroup({
    NombreAlumno: new FormControl(''),
    NombreProyecto: new FormControl(''),
    tipoProyecto: new FormControl(''),
    descripcion: new FormControl(''),
  });

  formSubmitted = false;  // Variable para saber si el formulario fue enviado
  successMessage: string = '';  // Mensaje de éxito
  errorMessage: string = '';  // Mensaje de error

  constructor(private propuestaService: PropuestaService) { }

  submitApplication() {
    this.formSubmitted = true;  // Marcamos que se intentó enviar el formulario

    if (this.applyForm.invalid) {
      console.log('Formulario inválido');
      return;  // No enviar si el formulario no es válido
    }

    const propuestaData: PropuestaModel = {
      userId: -1,  // id del alumno
      titulo: this.applyForm.value.NombreProyecto ?? '',
      descripcion: this.applyForm.value.descripcion?? '',
      tipo: this.applyForm.value.tipoProyecto ?? '',
      estado: "Enviada"
    };

    this.propuestaService.postPropuesta(propuestaData).then(response => {
      console.log('Propuesta enviada:', response);
      this.successMessage = 'Propuesta enviada con éxito';  // Mensaje de éxito
      this.errorMessage = '';  // Limpiar mensaje de error si el envío fue exitoso
    }).catch(error => {
      console.error('Error al enviar la propuesta:', error);
      this.successMessage = '';  // Limpiar mensaje de éxito si hubo un error
      this.errorMessage = 'Hubo un error al enviar la propuesta, por favor inténtalo de nuevo.';  // Mensaje de error
    });
  }
}
