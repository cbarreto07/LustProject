import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ValidaVideoComponent } from './valida-video.component';

describe('ValidaVideoComponent', () => {
  let component: ValidaVideoComponent;
  let fixture: ComponentFixture<ValidaVideoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ValidaVideoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ValidaVideoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
