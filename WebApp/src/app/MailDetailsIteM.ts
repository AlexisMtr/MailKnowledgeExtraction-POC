import { MailItem } from "./MailItem";
import { MailDocument } from "./MailDocument";

export class MailDetailsItem {
    public mail : MailItem;
    public body : MailDocument;
    public documents : MailDocument[];
}