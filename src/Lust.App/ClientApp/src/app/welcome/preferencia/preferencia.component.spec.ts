import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PreferenciaComponent } from './preferencia.component';

describe('PreferenciaComponent', () => {
  let component: PreferenciaComponent;
  let fixture: ComponentFixture<PreferenciaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PreferenciaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PreferenciaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
