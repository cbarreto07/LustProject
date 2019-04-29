import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ValidaPerfilComponent } from './valida-perfil.component';

describe('ValidaPerfilComponent', () => {
  let component: ValidaPerfilComponent;
  let fixture: ComponentFixture<ValidaPerfilComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ValidaPerfilComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ValidaPerfilComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
