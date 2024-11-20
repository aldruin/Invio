import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { EquipesService } from '../equipes.service';

@Component({
  selector: 'app-equipes',
  templateUrl: './equipes.component.html',
  styleUrl: './equipes.component.css'
})
export class EquipesComponent implements OnInit {
  message: string | undefined;
  equipes: any[] = [];


  constructor(private equipeService: EquipesService, private router: Router) { }

  ngOnInit(): void {
    this.equipeService.getEquipes().subscribe({
      next: (response: any) => {
        this.equipes = response;
        if (this.equipes.length > 0) {
          this.message = `Equipes carregadas: ${this.equipes.length}`;
        } else {
          this.message = 'Nenhuma equipe encontrada.';
        }
      },
      error: error => console.log(error)
    });
  }

  irParaInicio(): void {
    this.router.navigate(['']);
  }

  criarEquipe(): void {
    this.router.navigate(['equipes/criar']);
  }

  irParaDetalhes(id: number): void {
    this.router.navigate(['equipes', id]);
  }

  editarEquipe(id: number): void {
    this.router.navigate(['equipes/editar', id]);
  }
}
