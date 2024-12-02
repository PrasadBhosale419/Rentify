import { Component, Input, ViewEncapsulation } from '@angular/core';
import { IPropertyBase } from '../../Model/Ipropertybase';

@Component({
  selector: 'app-property-card',
  templateUrl: './property-card.component.html',
  styleUrl: './property-card.component.css'
})
export class PropertyCardComponent {
  @Input() property:IPropertyBase
  @Input() hideIcons?:boolean;
}
