import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { NavbarComponent } from 'src/app/components/navbar/navbar.component';
import { AuthService } from 'src/app/service/auth.service';
import { PropuestaService } from 'src/app/service/propuesta.service';

@Component({
  selector: 'app-estado',
  imports: [NavbarComponent, CommonModule],
  templateUrl: './estado.component.html',
  styleUrls: ['./estado.component.css']
})
export class EstadoComponent {
//hacer get de propuesta y mostrar datos (el estado lo principal)
propuestas: any[] = [];
userId!: number;

constructor(private propuestaService: PropuestaService, private authService: AuthService) {}

ngOnInit() {
  const userId = this.authService.getUserId();
  if (userId !== null) {
    this.propuestaService.getPropuestasUsuario(userId).then(data => {
      this.propuestas = data;
    }).catch(error => console.error('Error obteniendo propuestas:', error));
  } else {
    console.warn("⚠️ No se pudo obtener el ID del usuario.");
  }
}
}