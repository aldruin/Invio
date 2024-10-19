import { Component, OnInit } from '@angular/core';
import { EquipesService } from './equipes.service';

@Component({
  selector: 'app-equipes',
  templateUrl: './equipes.component.html',
  styleUrl: './equipes.component.css'
})
export class EquipesComponent implements OnInit {
  message: string | undefined;


  constructor(private equipeService: EquipesService ){}

  ngOnInit(): void {
    this.equipeService.getEquipes().subscribe({
      next: (response: any) => this.message = response.value.message,
      error: error => console.log(error)
    })
  }

}
