import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactAddPageComponent } from './contact-add-page.component';

describe('ContactAddPageComponent', () => {
  let component: ContactAddPageComponent;
  let fixture: ComponentFixture<ContactAddPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ContactAddPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ContactAddPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
