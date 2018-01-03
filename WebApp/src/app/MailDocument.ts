export class MailDocument {
    public name : string;
    public technologies : string[];
    public industryTerms : string[];
    public urls : string[];
    public topics : Topic[];
    public tags : Tag[];
    public companies : Company[];
    public persons : Person[];
    public contacts : Contact[];
}

export class Topic {
    public name : string;
    public score : number;
}

export class Tag {
    public value : string;
    public importance : number;
}

export class Company {
    public name : string;
    public permIdLink : string;
}

export class Person {
    public name : string;
    public emailAddress : string;
    public phoneNumber : string;
}

export class Contact {
    public telephone : string;
    public emailAddress : string;
}