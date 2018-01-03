import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MailDocumentCmpComponent } from './mail-document-cmp.component';

describe('MailDocumentCmpComponent', () => {
  let component: MailDocumentCmpComponent;
  let fixture: ComponentFixture<MailDocumentCmpComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MailDocumentCmpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MailDocumentCmpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
