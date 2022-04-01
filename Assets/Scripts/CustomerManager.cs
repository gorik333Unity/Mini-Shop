using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    private Customer[] _customer;

    [SerializeField]
    private Transform _startPoint;

    [SerializeField]
    private Transform _endPoint;

    [SerializeField]
    private float _cooldown;

    private readonly List<Customer> _availableCustomer = new List<Customer>();

    private int _currentCustomerCount;

    private void Awake()
    {
        InitializeActions();
    }

    private void Start()
    {
        _availableCustomer.AddRange(_customer);

        StartCoroutine(IEComeToPoint());
    }

    private void InitializeActions()
    {
        for (int i = 0; i < _customer.Length; i++)
            _customer[i].OnFull += Customer_OnFull;
    }

    private void Customer_OnFull(Customer customer)
    {
        customer.Agent.SetDestination(_startPoint.position);

        StartCoroutine(IECheckIfCustomerOnStartPoint(customer));
    }

    private void OnReturnToStartPoint(Customer customer)
    {
        customer.ClearPurchases();

        _availableCustomer.Add(customer);
    }

    private IEnumerator IECheckIfCustomerOnStartPoint(Customer customer)
    {
        while (true)
        {
            Vector3 a = customer.transform.position;
            Vector3 b = _startPoint.position;

            if ((a - b).sqrMagnitude < 1f)
            {
                OnReturnToStartPoint(customer);

                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator IEComeToPoint()
    {
        while (true)
        {
            yield return new WaitUntil(() => _availableCustomer.Count > 0);

            Customer customer = _availableCustomer[_currentCustomerCount];
            customer.Agent.SetDestination(_endPoint.position);

            _currentCustomerCount++;
            _availableCustomer.Remove(customer);

            if (_currentCustomerCount >= _availableCustomer.Count)
                _currentCustomerCount = 0;

            yield return new WaitForSeconds(_cooldown);
        }
    }
}
