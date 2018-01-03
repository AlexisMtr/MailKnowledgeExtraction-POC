import { Component, OnInit, Input } from '@angular/core';
import { MailDocument } from '../MailDocument';

@Component({
  selector: 'app-mail-document-cmp',
  templateUrl: './mail-document-cmp.component.html',
  styleUrls: ['./mail-document-cmp.component.css']
})
export class MailDocumentCmpComponent implements OnInit {

    @Input() item : MailDocument;
    constructor() { }

    ngOnInit() {
    }

}
