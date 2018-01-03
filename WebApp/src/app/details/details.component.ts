import { Component, OnInit, Input } from '@angular/core';
import { MailDetailsItem } from '../MailDetailsItem';
import { MailDocument } from '../MailDocument';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

    @Input() item : MailDetailsItem;
    public selectedDocument : MailDocument;
    constructor() { }

    ngOnInit() {
    }

    public OnSelect(d: MailDocument) : void {
        this.selectedDocument = d;
    }

}
