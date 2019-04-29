import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ValidaFotoComponent } from './valida-foto.component';

describe('ValidaFotoComponent', () => {
  let component: ValidaFotoComponent;
  let fixture: ComponentFixture<ValidaFotoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ValidaFotoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ValidaFotoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
