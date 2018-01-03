import { Component, OnInit } from '@angular/core';
import { MailItem } from './MailItem';
import { MailDetailsItem } from './MailDetailsItem';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    title = 'app';

    public Items : MailItem[];
    public Mail : MailDetailsItem;

    constructor(private client: HttpClient) { }

    ngOnInit(): void {
        let header = new HttpHeaders();
        header.append('Access-Control-Allow-Origin', '*');

        this.client.get<MailItem[]>("http://localhost:51913/api/document", {
            headers: header,
        }).subscribe(res => this.Items = res, (e) => console.error(e), () => this.OnSelect(this.Items[0]));
    }

    public OnSelect(item: MailItem) : void {
        this.client.get<MailDetailsItem>("http://localhost:51913/api/document/" + item.id)
            .subscribe(res => this.Mail = res, (e) => console.error(e), () => console.log(this.Mail));
    }
}
