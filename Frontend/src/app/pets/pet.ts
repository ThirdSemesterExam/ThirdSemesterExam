import {Component, OnInit, Pipe, PipeTransform} from '@angular/core';
import {HttpService} from "../../services/http.service";

@Component({
  selector: 'app-pets',
  templateUrl: './pet.html',
  styleUrls: ['./pet.scss']
})
export class Pet {

  constructor(public http: HttpService) {}

  petName: string = "";
  petPrice: number = 0;
  petDescription: string = "";
  DogBreeds: string = "";
  Address: string = "";
  zipcode:number = 0;
  city: string = "";
  email: string = "";
  image: string = "";


  toggleExpand(product: any) {
    product.expanded = !product.expanded;
  }
}
@Pipe({
  name: 'filter',
  pure: false
})
export class FilterPipe implements PipeTransform {
  transform(items: any[], searchText: string): any[] {
    if (!items) { return []; }
      return items.filter(item => {
        if (item) {
          return (item["name"].toLowerCase().includes(searchText));
        }
        return false;
      });
  }
}
