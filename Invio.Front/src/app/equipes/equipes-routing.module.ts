import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AcessarComponent } from './acessar/acessar.component';
import { EditarComponent } from './editar/editar.component';
import { RemoverComponent } from './remover/remover.component';
import { EquipesComponent } from './listagem/equipes.component';
import { CriarComponent } from './criar/criar.component';

const routes: Routes = [
  { path: 'acessar', component: AcessarComponent },
  { path: 'editar', component: EditarComponent },
  { path: 'remover', component: RemoverComponent },
  { path: 'listar', component: EquipesComponent },
  { path: 'criar', component: CriarComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EquipesRoutingModule { }
