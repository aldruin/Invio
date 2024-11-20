import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EquipesRoutingModule } from './equipes-routing.module';
import { RemoverComponent } from './remover/remover.component';
import { EditarComponent } from './editar/editar.component';
import { CriarComponent } from './criar/criar.component';
import { AcessarComponent } from './acessar/acessar.component';


@NgModule({
  declarations: [
    RemoverComponent,
    EditarComponent,
    CriarComponent,
    AcessarComponent
  ],
  imports: [
    CommonModule,
    EquipesRoutingModule
  ]
})
export class EquipesModule { }
