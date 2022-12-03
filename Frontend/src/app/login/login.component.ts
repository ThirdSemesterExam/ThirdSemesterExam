import { Component } from '@angular/core';
import {HttpService} from "../../services/http.service";


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent  {

  email: any;
  password: any;
  htmlSnippet: string =
    "<script>alert(\"attempt to hack\")</script><b>other html tag</b>";

  constructor(public http: HttpService) { }
}


