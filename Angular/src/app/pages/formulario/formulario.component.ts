import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-formulario',
  imports: [ReactiveFormsModule],
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

  submitApplication() {
    console.log("Datos del formulario:", this.applyForm.value);
  }
}
