import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PropuestaService } from 'src/app/service/propuesta.service';
import { PropuestaModel } from '../../models/propuestaModel';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from 'src/app/components/navbar/navbar.component';
import { AuthService } from 'src/app/service/auth.service'; // ✅ Importar AuthService

@Component({
  selector: 'app-formulario',
  imports: [ReactiveFormsModule, CommonModule, NavbarComponent],
  standalone: true,
  templateUrl: './formulario.component.html',
  styleUrls: ['./formulario.component.css']
})
export class FormularioComponent {
  applyForm = new FormGroup({
    NombreAlumno: new FormControl('', [Validators.required, Validators.minLength(5)]),
    NombreProyecto: new FormControl('', [Validators.required, Validators.minLength(5)]),
    tipoProyecto: new FormControl('', [Validators.required]),
    descripcion: new FormControl('', [Validators.required, Validators.minLength(25)]),
  });

  formSubmitted = false;
  successMessage: string = '';
  errorMessage: string = '';
  userEmail: string = ''; // ✅ Variable para almacenar el email del usuario

  constructor(
    private propuestaService: PropuestaService,
    private authService: AuthService // ✅ Inyectar AuthService
  ) {
    this.userEmail = this.authService.getUserEmail() || ''; // ✅ Obtener el email
  }

  submitApplication() {
    this.formSubmitted = true;

    if (this.applyForm.invalid) {
      console.log('Formulario inválido');
      return;
    }

    const propuestaData: PropuestaModel = {
      email: this.userEmail, // ✅ Usar el email del usuario autenticado
      titulo: this.applyForm.value.NombreProyecto ?? '',
      descripcion: this.applyForm.value.descripcion ?? '',
      tipo: this.applyForm.value.tipoProyecto ?? '',
      estado: 'Enviada'
    };

    this.propuestaService.postPropuesta(propuestaData).then(response => {
      console.log('Propuesta enviada:', response);
      this.successMessage = 'Propuesta enviada con éxito';
      this.errorMessage = '';
    }).catch(error => {
      console.error('Error al enviar la propuesta:', error);
      this.successMessage = '';
      this.errorMessage = 'Hubo un error al enviar la propuesta, por favor inténtalo de nuevo.';
    });
  }

  hasError(field: string): boolean {
    return this.applyForm.get(field)?.invalid && this.applyForm.get(field)?.touched || this.formSubmitted;
  }
}
