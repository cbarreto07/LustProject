import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowErrosDialogComponent } from './show-erros-dialog.component';

describe('ShowErrosDialogComponent', () => {
  let component: ShowErrosDialogComponent;
  let fixture: ComponentFixture<ShowErrosDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShowErrosDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowErrosDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
