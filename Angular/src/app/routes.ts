import { Routes } from '@angular/router';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { HomeComponent } from './pages/home/home.component';
import { FormularioComponent } from './pages/formulario/formulario.component';


const routeConfig: Routes = [
  {
    path: '',
    component: HomeComponent,
    title: 'Home page',
  },
  {
    path: 'formulario',
    component: FormularioComponent,
    title: 'formulario',
  },
  {
    path: '**',
    component: PageNotFoundComponent,
  },
];

export default routeConfig;
