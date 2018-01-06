import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { MailDetailsItem } from '../MailDetailsItem';
import { MailDocument } from '../MailDocument';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit, OnChanges {

    @Input() item : MailDetailsItem;
    public selectedDocument : MailDocument;
    constructor() { }

    ngOnInit() {
        this.selectedDocument = null;
    }

    ngOnChanges(changes: any) {
        this.selectedDocument = this.item.body;
    }

    public OnSelect(d: MailDocument) : void {
        this.selectedDocument = d;
    }

}
