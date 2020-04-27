import React, { useState, useEffect } from 'react';
import { Card, Steps } from 'antd';
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import { connect } from 'umi';
import { PostFinancialFormStateType } from './model';
import Step1 from './components/Step1';
import Step2 from './components/Step2';
import Step3 from './components/Step3';
import styles from './style.less';

const { Step } = Steps;

interface FinancialFormProps {
  current: PostFinancialFormStateType['current'];
}

const getCurrentStepAndComponent = (current?: string) => {
  switch (current) {
    case 'input':
      return { step: 1, component: <Step2 /> };
    case 'finished':
      return { step: 2, component: <Step3 /> };
    case 'select':
    default:
      return { step: 0, component: <Step1 /> };
  }
};

const FinancialForm: React.FC<FinancialFormProps> = ({ current }) => {
  const [stepComponent, setStepComponent] = useState<React.ReactNode>(<Step1 />);
  const [currentStep, setCurrentStep] = useState<number>(0);

  useEffect(() => {
    const { step, component } = getCurrentStepAndComponent(current);
    setCurrentStep(step);
    setStepComponent(component);
  }, [current]);
  return (
    <PageHeaderWrapper>
      <Card bordered={false}>
        <>
          <Steps current={currentStep} className={styles.steps}>
            <Step title="选择相应的申请" />
            <Step title="输入财务表单信息" />
            <Step title="完成" />
          </Steps>
          {stepComponent}
        </>
      </Card>
    </PageHeaderWrapper>
  );
};

export default connect(({
  postFinancialForm
}: {
  postFinancialForm: PostFinancialFormStateType
}) => ({
  current: postFinancialForm.current,
}))(FinancialForm);
