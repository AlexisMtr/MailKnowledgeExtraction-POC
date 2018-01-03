import { Component, OnInit, Input } from '@angular/core';
import { MailItem } from '../MailItem';

@Component({
    selector: 'app-master',
    templateUrl: './master.component.html',
    styleUrls: ['./master.component.css']
})
export class MasterComponent implements OnInit {

    @Input() Items : MailItem[];
    constructor() { }

    ngOnInit() {
        this.Items = [
            {
                id: "",
                sender: "a.martinier@gmail.com",
                object: "Test",
                receivedOn: new Date("2018-01-02T18:56:11.981Z"),
                attachmentsCount: 2 
            },
            {
                id: "",
                sender: "a.martinier@gmail.com",
                object: "Test",
                receivedOn: new Date("2018-01-02T18:56:11.981Z"),
                attachmentsCount: 2 
            },
            {
                id: "",
                sender: "a.martinier@gmail.com",
                object: "Test",
                receivedOn: new Date("2018-01-02T18:56:11.981Z"),
                attachmentsCount: 2 
            },
            {
                id: "",
                sender: "a.martinier@gmail.com",
                object: "Test",
                receivedOn: new Date("2018-01-02T18:56:11.981Z"),
                attachmentsCount: 2 
            }
        ];
    }

}
