import { Product, ProductService } from '../../services/product.service';
import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

declare var bootstrap: any;

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule, FormsModule], 
  templateUrl: './products.component.html'
})
export class ProductsComponent implements OnInit {
  
  products = signal<Product[]>([]);
  
  product: Product = { name: '', price: 0, company: '', ageRestriction: 0, description: '' };
  isEdit = false;
  modalInstance: any;

  constructor(private service: ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this.service.getAll().subscribe(data => {
      this.products.set(data);
    });
  }

  deleteProduct(id: number) {
    if(confirm('¿Estás seguro de eliminar este producto? Esta acción no se puede deshacer.')) {
      this.service.delete(id).subscribe(() => {
        this.loadProducts();
      });
    }
  }

  openModal() {
    const modalElement = document.getElementById('productModal');
    if (modalElement) {
      this.modalInstance = new bootstrap.Modal(modalElement);
      this.modalInstance.show();
    }
  }

  closeModal() {
    if (this.modalInstance) {
      this.modalInstance.hide();
    }
  }

  addNew() {
    this.isEdit = false;
    this.product = { name: '', price: 0, company: '', ageRestriction: 0, description: '' };
    this.openModal();
  }

  updateProduct(id: number) {
    this.isEdit = true;
    this.service.getById(id).subscribe(data => {
      this.product = data;
      this.openModal();
    });
  }

  save(): void {
    if (this.isEdit && this.product.id) {
      this.service.update(this.product.id, this.product).subscribe(() => {
        this.loadProducts();
        this.closeModal();
      });
    } else {
      this.service.create(this.product).subscribe(() => {
        this.loadProducts();
        this.closeModal();
      });
    }
  }
}