import { Component } from '@angular/core';
import {HttpService} from "../../services/http.service";


@Component({
  selector: 'app-admin-login'+ 'ngbd-dropdown-basic',
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.scss']
})
export class AdminLoginComponent  {

  email: any;
  password: any;
  htmlSnippet: string =
    "<script>alert(\"attempt to hack\")</script><b>other html tag</b>";

  constructor(public http: HttpService) { }
}


